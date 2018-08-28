﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace SWYRA_Movil
{
    public partial class FrmMenuPedidosArea : Form
    {
        public string cvedoc;
        private Pedidos ped = new Pedidos();
        private List<DetallePedidos> det = new List<DetallePedidos>();
        private List<DetallePedidos> dev = new List<DetallePedidos>();
        private List<DetallePedidos> detA = new List<DetallePedidos>();
        private List<DetallePedidos> devA = new List<DetallePedidos>();

        public FrmMenuPedidosArea()
        {
            InitializeComponent();
        }

        private void pbSalir_Click(object sender, EventArgs e)
        {
            if (!validaExis(false))
            {
                ped.solarea = false;
                var query = "UPDATE PEDIDO SET SOLAREA = 0 " +
                            "WHERE LTRIM(CVE_DOC) = '" + ped.cve_doc + "'";
                var r = Program.GetExecute(query, 9);
                MessageBox.Show(@"Existe Artículos por Agregar o Devolver fuera del área de Brocas. Favor de validar.", "SWYRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            this.Close();
        }

        private void pbConcluir_Click(object sender, EventArgs e)
        {
            if(!validaExis(false))
            {
                ped.solarea = false;
                var query = "UPDATE PEDIDO SET SOLAREA = 0 " +
                            "WHERE LTRIM(CVE_DOC) = '" + ped.cve_doc + "'";
                var r = Program.GetExecute(query, 9);
                MessageBox.Show(@"Existe Artículos por Agregar o Devolver fuera del área de Brocas. Favor de validar.", "SWYRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                this.Close();
            }
            if (!validaExis(true))
            {
                MessageBox.Show(@"Existe Artículos por Agregar o Devolver del área de Brocas. Favor de validar.", "SWYRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }
            try
            {
                FrmAreaEmpaque frmAreaEmp = new FrmAreaEmpaque();
                frmAreaEmp.lblPedido.Text = ped.cve_doc;
                frmAreaEmp.listBox1.Visible = false;
                frmAreaEmp.pbSig.Visible = false;
                frmAreaEmp.pbAnt.Visible = false;
                DialogResult dr = new DialogResult();
                if (ped.estatuspedido != "DEVOLUCION")
                {
                    dr = frmAreaEmp.ShowDialog();
                }
                else
                {
                    dr = DialogResult.OK;
                }
                if (dr == DialogResult.OK)
                {
                    var estatus = (ped.estatuspedido == "DEVOLUCION") ? "CANCELACION" : "EMPAQUE";
                    var query = "UPDATE PEDIDO SET ESTATUSPEDIDO = '" + estatus + "' " +
                                "WHERE LTRIM(CVE_DOC) = '" + ped.cve_doc + "' ";
                    var r = Program.GetExecute(query, 10);
                    query = "declare @cvedoc varchar(20) select @cvedoc = cve_doc from PEDIDO " +
                            "where LTRIM(CVE_DOC) = '" + ped.cve_doc + "' " +
                            "insert into PEDIDO_HIST (CVE_DOC, ESTATUSPEDIDO, FECHAMOV, USUARIO) values (" +
                            "@cvedoc, '" + estatus + "', getdate(), '" + Program.usActivo.Usuario + "')";
                    r = Program.GetExecute(query, 11);
                    var query2 = "declare @cvedoc varchar(20) select @cvedoc = cve_doc from PEDIDO " +
                                 "where LTRIM(CVE_DOC) = '" + ped.cve_doc + "' " +
                                 "DECLARE @num INT " +
                                 "SELECT @num = ISNULL(MAX(ID),0) + 1 FROM IMPRESION " +
                                 "INSERT IMPRESION (ID, FECHA, CVE_DOC, CVE_IMP, IMPRESION) " +
                                 "VALUES ( @num, GETDATE(), @cvedoc, 1, 'HOJA DE SURTIDO')";
                    var r2 = Program.GetExecute(query2, 13);
                    MessageBox.Show(@"Guardado satisfactoriamente.", "SWYRA", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SWYRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

        private void pbIncompletos_Click(object sender, EventArgs e)
        {
            FrmIncompleto frmIncompleto = new FrmIncompleto();
            frmIncompleto.ped = ped;
            frmIncompleto.det = detA.Where(o => o.cantpendiente > 0).ToList();
            frmIncompleto.pbRegresar.Visible = false;
            frmIncompleto.pbRegresarB.Visible = true;
            frmIncompleto.ShowDialog();
            pbConcluir.Visible = validaExis(false) && validaExis(true);
        }

        private void pbDevolucion_Click(object sender, EventArgs e)
        {
            FrmDevolucion frmDevolucion = new FrmDevolucion();
            frmDevolucion.ped = ped;
            frmDevolucion.Area = true;
            frmDevolucion.ShowDialog();
            pbConcluir.Visible = validaExis(false) && validaExis(true);
        }

        private void pbImprimir_Click(object sender, EventArgs e)
        {
            FrmSurtit frmSurtir = new FrmSurtit();
            frmSurtir.ped = ped;
            frmSurtir.det = detA.Where(o => o.surtido == false).ToList();
            frmSurtir.pbIncompleto.Visible = false;
            frmSurtir.pbIncompletoB.Visible = true;
            frmSurtir.ShowDialog();
            pbConcluir.Visible = validaExis(false) && validaExis(true);
        }

        private void FrmMenuPedidosArea_Load(object sender, EventArgs e)
        {
            try
            {
                var query = "select LTRIM(CVE_DOC) CVE_DOC, LTRIM(CVE_CLPV) CVE_CLPV, c.NOMBRE Cliente, LTRIM(p.CVE_VEND) CVE_VEND, " +
                            "TIPOSERVICIO, PRIORIDAD, ISNULL(SOLAREA,0) SOLAREA, ESTATUSPEDIDO, IMPORTE, UbicacionEmpaque, NOMBRE_VENDEDOR, " +
                            "CAPTURO, u.Nombre CAPTURO_N, OCURREDOMICILIO from PEDIDO p join CLIENTE c on p.CVE_CLPV = c.CLAVE " +
                            "left join USUARIOS u on u.Usuario = p.CAPTURO WHERE LTRIM(CVE_DOC) = '" + cvedoc + "'";
                ped = Program.GetDataTable(query, 1).ToData<Pedidos>();
                txtPedido.Text = ped.cve_doc;
                txtCliente.Text = "(" + ped.cve_clpv + ") " + ped.cliente;
                txtServicio.Text = ped.tiposervicio;
                txtOcurrDom.Text = ped.ocurredomicilio;
                txtVendedor.Text = ped.nombre_vendedor;
                txtCapturo.Text = ped.capturo_n;
                CultureInfo culture = new CultureInfo("es-MX");
                txtMonto.Text = ped.importe.ToString("C2", culture);
                if (ped.estatuspedido == "DEVOLUCION") { lblInfo.Text = "POR CANCELAR"; }

                pbConcluir.Visible = validaExis(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SWYRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

        private bool validaExis(bool Area)
        {
            bool res = false;
            try
            {
                if (Area)
                {
                    detA = Program.GetDataTable(createQR(Area, false), 2).ToList<DetallePedidos>();
                    devA = Program.GetDataTable(createQR(Area, true), 3).ToList<DetallePedidos>();
                    var cdetA = detA.Where(o => o.surtido == false).ToList().Count;
                    var cdevA = devA.Where(o => o.devuelto == false).ToList().Count;
                    res = (cdetA == 0 && cdevA == 0);
                    lblPendS.Text = cdetA.ToString();
                    lblPendM.Text = cdevA.ToString();
                }
                else
                {
                    det = Program.GetDataTable(createQR(Area, false), 4).ToList<DetallePedidos>();
                    dev = Program.GetDataTable(createQR(Area, true), 5).ToList<DetallePedidos>();
                    var cdet = det.Where(o => o.surtido == false).ToList().Count;
                    var cdev = dev.Where(o => o.devuelto == false).ToList().Count;
                    res = (cdet == 0 && cdev == 0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SWYRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            return res;
        }

        private string createQR(bool Area, bool Devuelto)
        {
            var query = "select a.*, isnull(o.ORDEN,0) ORDEN FROM( select *, " +
                    "case when sel > 0 then cast(((CANT - (ISNULL(" + (Devuelto ? "CANTDEVUELTO" : "CANTSURTIDO") + ",0) + CANTPENDIENTE) - 1) / sel) as int) else 0 end con, " +
                    "case when (case when sel > 0 then cast(((CANT - (ISNULL(" + (Devuelto ? "CANTDEVUELTO" : "CANTSURTIDO") + ",0) + CANTPENDIENTE) - 1) / sel) as int) else 1 end) > 0 then CTRL_ALM " +
                    "else case when MASTERS_UBI = '' then CTRL_ALM else MASTERS_UBI end end ubicacion, " +
                    "(CANT - (sel * (case when sel > 0 then cast(((CANT - (ISNULL(" + (Devuelto ? "CANTDEVUELTO" : "CANTSURTIDO") + ",0) + CANTPENDIENTE) - 1) / sel) as int) " +
                    "else 0 end)) - (ISNULL(" + (Devuelto ? "CANTDEVUELTO" : "CANTSURTIDO") + ",0) + CANTPENDIENTE)) CANTDIFERENCIA " +
                    "from ( SELECT  CVE_DOC, NUM_PAR, dp.CVE_ART, CANT, ISNULL(" + (Devuelto ? "CANTDEVUELTO" : "CANTSURTIDO") + ",0) " + (Devuelto ? "CANTDEVUELTO" : "CANTSURTIDO") + ", " +
                    "ISNULL(" + (Devuelto ? "DEVUELTO" : "SURTIDO") + ",0) " + (Devuelto ? "DEVUELTO" : "SURTIDO") + ", i.DESCR, i.EXIST, i.LIN_PROD, " +
                    "ISNULL(ic.COMENTARIO,'') COMENTARIO, ISNULL(ic.APLICAEXIST,0) APLICAEXIST, " +
                    "ISNULL(ic.EXISTENCIA,0) MINEXIST, ISNULL(IC.APLICALOTE,0) APLICALOTE, " +
                    "i.CTRL_ALM, i.MASTERS_UBI, i.UNI_EMP MIN, i.MASTERS MAS, " +
                    "cast(cast(case when i.MASTERS > 0 then (CANT / i.MASTERS) else 0 end as int) * i.MASTERS as int) sel, " +
                    (Devuelto ? "0" : "ISNULL(CANTPENDIENTE,0)") + " CANTPENDIENTE, cast(0 as bit) SW " +
                    "FROM " + (Devuelto ? "DETALLEPEDIDODEV" : "DETALLEPEDIDO") + " dp JOIN INVENTARIO i ON dp.CVE_ART = i.CVE_ART " +
                    "LEFT JOIN INVENTARIOCOND ic ON ic.CVE_ART = dp.CVE_ART AND ic.ACTIVO = 1 " +
                    "WHERE (LTRIM(CVE_DOC) = '" + ped.cve_doc + "') AND dp.NUM_PAR < 1000 ) as c) as a LEFT JOIN ORDEN_RUTA o ON RTRIM(LTRIM(a.ubicacion)) = o.CVE_UBI " +
                    "JOIN AREAS r ON ISNULL(o.AREA,'') " + (Area ? "" : "NOT") + " like '%' + r.NOMBRE + '%' ORDER BY o.ORDEN";
            return query;
        }

        private void pbDevolucion_Click_1(object sender, EventArgs e)
        {
            FrmCancelacion frmCan = new FrmCancelacion();
            frmCan.ped = ped;
            frmCan.det = devA.Where(o => o.devuelto == false).ToList();
            frmCan.ShowDialog();
            pbConcluir.Visible = validaExis(false) && validaExis(true);
        }

        private void pbDetener_Click(object sender, EventArgs e)
        {
           try
            {
                FrmDetenerPed frmDetenerPed = new FrmDetenerPed();
                frmDetenerPed.lblPedido.Text = ped.cve_doc.Trim();
                var rs = frmDetenerPed.ShowDialog();
                if (rs == DialogResult.OK)
                {
                    FrmDetenerPedCausa frmCausa = new FrmDetenerPedCausa();
                    frmCausa.lblPedido.Text = ped.cve_doc.Trim();
                    frmCausa.ShowDialog();
                    var query = "UPDATE PEDIDO SET ESTATUSPEDIDO = 'DETENIDO', " +
                                "CAUSADETENIDO = '" + frmCausa.txtCausa.Text + "' " +
                                "WHERE LTRIM(CVE_DOC) = '" + ped.cve_doc + "'";
                    var r = Program.GetExecute(query, 6);
                    query = "declare @cvedoc varchar(20) select @cvedoc = cve_doc from PEDIDO " +
                            "where LTRIM(CVE_DOC) = '" + ped.cve_doc + "' " +
                            "insert into PEDIDO_HIST (CVE_DOC, ESTATUSPEDIDO, FECHAMOV, USUARIO) values (" +
                            "@cvedoc, 'DETENIDO', getdate(), '" + Program.usActivo.Usuario + "')";
                    r = Program.GetExecute(query, 7);
                    MessageBox.Show(@"Guardado satisfactoriamente.", "SWYRA", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
                    frmDetenerPed.Close();
                    Close();
                }
                else
                {
                    frmDetenerPed.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SWYRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
    }
}