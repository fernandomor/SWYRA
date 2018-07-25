﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SWYRA_Movil
{
    public partial class FrmSurtit : Form
    {
        public Pedidos ped = new Pedidos();
        public List<DetallePedidos> det = new List<DetallePedidos>();
        public List<DetallePedidos> mostrardet = new List<DetallePedidos>();
        private CodigosBarra cod = new CodigosBarra();
        private DetallePedidos art = new DetallePedidos();
        private DetallePedidos artFirst = new DetallePedidos();
        private DetallePedidos artLast = new DetallePedidos();
        private string lastCB;
        private string Lote;
        private List<OrdenUbicacion> orbi = new List<OrdenUbicacion>();

        public FrmSurtit()
        {
            InitializeComponent();
        }

        private void pbSalir_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmSurtit_Load(object sender, EventArgs e)
        {
            CargaUbicaciones();
            mostrardet = det.Where(o => o.surtido == false).ToList();
            lblPedido.Text = ped.cve_doc.Trim();
            lblComentario.Text = "";
            art = mostrardet.FirstOrDefault();
            artFirst = art;
            artLast = mostrardet.LastOrDefault();
            lblPendientes.Text = mostrardet.Count.ToString();
            cargaDatos();
            txtCodigo.Focus();
        }

        private void cargaDatos()
        {
            txtCant.Value = 1;
            txtUbica.Text = (art != null) ? art.ubicacion : "";
            txtClave.Text = (art != null) ? art.cve_art : "";
            txtDescr.Text = (art != null) ? art.descr : "";
            txtMinimo.Text = (art != null) ? art.min.ToString() : "";
            txtMaster.Text = (art != null) ? art.mas.ToString() : "";
            txtPorSurtir.Text = (art != null) ? art.cantdiferencia.ToString() : "";
            txtSurtido.Text = (art != null) ? art.cantsurtido.ToString() : "";
            txtExistencia.Text = (art != null) ? art.exist.ToString() : "";
            lblComentario.Text = (art != null) ? art.comentario : "";
            txtMasterUbi.Text = (art != null) ? art.masters_ubi : "";
        }

        private void CargaUbicaciones()
        {
            try
            {
                var query = "select * from orden_ruta order by ORDEN";
                orbi = Program.GetDataTable(query, 4).ToList<OrdenUbicacion>();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SWYRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) || (e.KeyChar == (char)Keys.Return))
            {
                txtMinimo.Focus();
                e.Handled = true;
            }
        }

        private void txtCodigo_LostFocus(object sender, EventArgs e)
        {
            if (txtCodigo.Text == "")
            {
                return;
            }
            lastCB = txtCodigo.Text;
            txtCant.Value = 1;;
            try
            {
                var str = txtCodigo.Text.Split('-');
                var query = "SELECT CVE_ART, CANT_PIEZAS, CODIGO_BARRA FROM vw_codigosBarras " +
                             "WHERE CODIGO_BARRA = '" + str[0] + "'";
                cod = Program.GetDataTable(query, 1).ToData<CodigosBarra>();
                if (str.Length == 2)
                {
                    txtCodigo.Text = str[0];
                    cod.cant_piezas = Convert.ToInt32(str[1]);
                }
                if (cod != null && str.Length <= 2)
                {
                    try
                    {
                        if (art.cve_art != cod.cve_art)
                        {
                            MessageBox.Show(@"Código NO coincide con el artículo a surtir", "SWYRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        }
                        else
                        {
                            if (cod.cant_piezas > art.exist)
                            {
                                MessageBox.Show(@"Artículo NO HAY EN EXISTENCIA.", "SWYRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                            }
                            else
                            {
                                DialogResult drm = DialogResult.OK;
                                if (cod.cant_piezas > art.cantdiferencia)
                                {
                                    var res = "La Presentación del artículo (" + art.cve_art + ") " + art.descr + " excede a la cantidad por surtir. Favor de validar con el ANALISTA DE VENTAS.";
                                    MessageBox.Show(res, "SWYRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                                    drm = DialogResult.No;
                                }
                                if (drm == DialogResult.OK)
                                {
                                    DialogResult dr = DialogResult.Cancel;
                                    if (art.lin_prod.Contains("EXHIB"))
                                    {
                                        FrmSurtir2 frmConf1 = new FrmSurtir2();
                                        dr = frmConf1.ShowDialog();
                                        frmConf1.Close();
                                    }
                                    if (art.aplicalote)
                                    {
                                        FrmSurtir3 frmConf2 = new FrmSurtir3();
                                        frmConf2.ShowDialog();
                                        Lote = frmConf2.txtLote.Text;
                                        frmConf2.Close();
                                    }
                                    //txtCant.ReadOnly = !(cod.cant_piezas == 1 || dr == DialogResult.Cancel);
                                    txtCant.Value = cod.cant_piezas;
                                    actualizaDet();
                                    //if (cod.cant_piezas == 1) { txtCant.Focus(); }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(@"Artículo NO ASIGNADO al pedido.", "SWYRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    }
                }
                else
                {
                    MessageBox.Show(@"Código de Barra INEXISTENTE, favor de validar.", "SWYRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SWYRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            if (txtCant.ReadOnly)
            {
                txtCodigo.Text = "";
                txtCodigo.Focus();
            }
        }

        private void txtCant_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) || (e.KeyChar == (char)Keys.Return))
            {
                if (!txtCant.ReadOnly)
                {
                    if ((double)txtCant.Value > art.cantdiferencia)
                    {
                        txtCant.Value = (decimal)art.cantdiferencia;
                        MessageBox.Show(@"Artículo excede a la cantidad solicitada.", "SWYRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    }
                    else
                    {
                        if ((double)txtCant.Value > art.exist)
                        {
                            txtCant.Value = (decimal)art.exist;
                            MessageBox.Show(@"Artículo EXCEDE EN EXISTENCIA.", "SWYRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        }
                        else
                        {
                            txtCant.ReadOnly = true;
                            txtCodigo.Text = "";
                            txtCodigo.Focus();
                            e.Handled = true;
                        }
                    }
                }
            }
        }

        private void actualizaDet()
        {
            if (txtCant.ReadOnly)
            {
                art.cantsurtido += (double)txtCant.Value;
                art.con = (art.sel > 0) ? (int)((art.cant - (int)(art.cantsurtido + art.cantpendiente) - 1) / art.sel) : 0;
                art.cantdiferencia = art.cant - (art.sel * art.con) - (art.cantsurtido + art.cantpendiente);
                art.surtido = (art.cant == (art.cantsurtido + art.cantpendiente));
                art.exist = (art.exist - (double)txtCant.Value);
                var ubiant = art.ubicacion;
                art.ubicacion = (art.sel == 0 && art.con == 0) ? art.ctrl_alm : ((art.con > 0) ? art.ctrl_alm : ((art.masters_ubi == "") ? art.ctrl_alm : art.masters_ubi));
                var orb = orbi.First(o => o.cve_ubi == art.ubicacion);
                art.orden = orb.orden;
                var confirmar = "IF EXISTS( SELECT * FROM DETALLEPEDIDOMERC WHERE CVE_DOC = '" + art.cve_doc + "' AND CODIGO_BARRA = '" + lastCB + "' AND ISNULL(CANCELADO,0) = 0) " +
                                "UPDATE DETALLEPEDIDOMERC SET CANT = CANT + " + txtCant.Value.ToString() + ", " +
                                "lote = (isnull(lote,'') + case when isnull(lote,'') <> '' then ';' else '' end + '" + Lote + "') " + 
                                "WHERE CVE_DOC = '" + art.cve_doc + "' AND CODIGO_BARRA = '" + lastCB + "' ELSE ";
                var query = "DECLARE @consec INT " +
                            "SELECT @consec = (ISNULL(MAX(CONSEC),-1) + 1) FROM DETALLEPEDIDOMERC " +
                            "WHERE CVE_DOC = '" + art.cve_doc + "' " + 
                            (cod.cant_piezas == 1 ? confirmar : "") +
                            "INSERT DETALLEPEDIDOMERC (CVE_DOC, CONSEC, NUM_PAR, CVE_ART, CODIGO_BARRA, CANT, lote) " +
                            "VALUES ('" + art.cve_doc + "', @consec, " + art.num_par.ToString() + ", '" + art.cve_art +
                            "', '" + lastCB + "', " + txtCant.Value.ToString() + ", '" + Lote + "') " +
                            "UPDATE DETALLEPEDIDO SET CANTSURTIDO = " + art.cantsurtido +
                            ", SURTIDO = " + ((art.surtido) ? "1" : "0") +
                            " WHERE CVE_DOC = '" + art.cve_doc + "' AND NUM_PAR = " + art.num_par.ToString() +
                            " UPDATE INVENTARIO SET EXIST = EXIST - " + txtCant.Value.ToString() + " WHERE CVE_ART = '" + art.cve_art + "' " +
                            "update PEDIDO set PORC_SURTIDO = r.porc from PEDIDO p join ( " +
                            "select CVE_DOC, (sum(CAST(ISNULL(SURTIDO,0) AS float)) / CAST(count(SURTIDO) as float)) * 100.0 porc from DETALLEPEDIDO " +
                            "where CVE_DOC = '" + art.cve_doc + "' group by CVE_DOC) as r ON p.CVE_DOC = r.CVE_DOC ";
                Program.GetExecute(query, 2);
                Lote = "";

                if (art.cantdiferencia == 0 || ubiant != art.ubicacion)
                {
                    mostrardet = det.Where(o => o.surtido == false).ToList();
                    mostrardet = mostrardet.OrderBy(o => o.orden).ToList();
                    art = mostrardet.FirstOrDefault();
                    artFirst = art;
                    artLast = mostrardet.LastOrDefault();
                    lblPendientes.Text = mostrardet.Count.ToString();
                }
                cargaDatos();
                txtCodigo.Focus();
            }
        }

        private void txtCant_LostFocus(object sender, EventArgs e)
        {
            //actualizaDet();
        }

        private void pbIncompleto_Click(object sender, EventArgs e)
        {
            if (art != null)
            {
                FrmSurtir4 frmConf = new FrmSurtir4();
                frmConf.lblCom.Text = "El artículo (" + art.cve_art + ") " + art.descr + " lo consideras INCOMPLETO, con " + art.cantdiferencia + " piezas restantes."; 
                var dr = frmConf.ShowDialog();
                frmConf.Close();
                if (dr == DialogResult.OK)
                {
                    var query = "IF EXISTS(SELECT * FROM DETALLEPEDIDOMERC WHERE CVE_DOC = '" + art.cve_doc + "' AND CVE_ART = '" + art.cve_art + "') " +
                                "BEGIN DECLARE @MAXCONSEC INT SELECT @MAXCONSEC = CONSEC FROM DETALLEPEDIDOMERC " +
                                "WHERE CVE_DOC = '" + art.cve_doc + "' AND CVE_ART = '" + art.cve_art + "' " +
                                "UPDATE DETALLEPEDIDOMERC SET PEND = " + art.cantdiferencia +
                                " WHERE CVE_DOC = '" + art.cve_doc + "' AND CVE_ART = '" + art.cve_art + "' AND CONSEC = @MAXCONSEC " +
                                "END ELSE BEGIN DECLARE @consec INT SELECT @consec = (ISNULL(MAX(CONSEC),-1) + 1) FROM DETALLEPEDIDOMERC " +
                                "WHERE CVE_DOC = '" + art.cve_doc + "' " +
                                "INSERT DETALLEPEDIDOMERC (CVE_DOC, CONSEC, NUM_PAR, CVE_ART, CODIGO_BARRA, CANT, PEND) " +
                                "VALUES ('" + art.cve_doc + "', @consec, " + art.num_par + ", '" + art.cve_art + "', '', 0, " + art.cantdiferencia + ") END";
                    Program.GetExecute(query, 100);

                    art.cantpendiente = art.cantpendiente + art.cantdiferencia;
                    art.con = (art.sel > 0) ? (int)((art.cant - (int)(art.cantsurtido + art.cantpendiente) - 1) / art.sel) : 0;
                    art.cantdiferencia = art.cant - (art.sel * art.con) - (art.cantsurtido + art.cantpendiente);
                    art.surtido = (art.cant == (art.cantsurtido + art.cantpendiente));
                    art.ubicacion = (art.sel == 0 && art.con == 0) ? art.ctrl_alm : ((art.con > 0) ? art.ctrl_alm : ((art.masters_ubi == "") ? art.ctrl_alm : art.masters_ubi));
                    var orb = orbi.First(o => o.cve_ubi == art.ubicacion);
                    art.orden = orb.orden;

                    query = 
                                "UPDATE DETALLEPEDIDO SET SURTIDO = " + ((art.surtido) ? "1" : "0") + ", CANTPENDIENTE = " + art.cantpendiente +
                                " WHERE CVE_DOC = '" + art.cve_doc + "' AND NUM_PAR = " + art.num_par.ToString() + " " +
                                "update PEDIDO set PORC_SURTIDO = r.porc from PEDIDO p join ( " +
                                "select CVE_DOC, (sum(CAST(ISNULL(SURTIDO,0) AS float)) / CAST(count(SURTIDO) as float)) * 100.0 porc from DETALLEPEDIDO " +
                                "where CVE_DOC = '" + art.cve_doc + "' group by CVE_DOC) as r ON p.CVE_DOC = r.CVE_DOC ";
                    Program.GetExecute(query, 3);
                    Lote = "";

                    mostrardet = det.Where(o => o.surtido == false).ToList();
                    art = mostrardet.FirstOrDefault();
                    artFirst = art;
                    artLast = mostrardet.LastOrDefault();
                    lblPendientes.Text = mostrardet.Count.ToString();
                    cargaDatos();
                }
            }
        }

        private void pbSig_Click(object sender, EventArgs e)
        {
            if (art != null)
            {
                if (art.num_par == artLast.num_par)
                {
                    art = artFirst;
                }
                else
                {
                    art = Program.GetNext<DetallePedidos>(mostrardet, art);
                }
                cargaDatos();
            }
        }

        private void pbAnt_Click(object sender, EventArgs e)
        {
            if (art != null)
            {
                if (art.num_par == artFirst.num_par)
                {
                    art = artLast;
                }
                else
                {
                    art = Program.GetPrevious<DetallePedidos>(mostrardet, art);
                }
                cargaDatos();
            }
        }
    }
}