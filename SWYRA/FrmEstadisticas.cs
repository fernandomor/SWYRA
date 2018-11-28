﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using static SWYRA.General;

namespace SWYRA
{
    public partial class FrmEstadisticas : Form
    {
        string queryConst = "";

        public FrmEstadisticas()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmEstadisticas_Load(object sender, EventArgs e)
        {
            txtFechFin.DateTime = DateTime.Now;
            txtFechIni.DateTime = DateTime.Now;
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            queryConst = "declare @fini datetime declare @ffin datetime " +
                         "select @ffin = '" + txtFechFin.DateTime.ToString("yyyyMMdd") + "' " +
                         "select @fini = '" + txtFechIni.DateTime.ToString("yyyyMMdd") + "' " +
                         "declare @dias int set @dias = [dbo].[fBusinessDays](@fini, @ffin) + 1 " +
                         "declare @ped table(cve_doc varchar(20)) insert @ped " +
                         "select CVE_DOC from PEDIDO where FECHA_DOC between @fini and @ffin and ESTATUSPEDIDO NOT IN ('CANCELACION') " +
                         "declare @pedcan int select @pedcan = count(CVE_DOC) from PEDIDO where FECHA_DOC between @fini and @ffin and ESTATUSPEDIDO IN('CANCELACION') " +
                         "declare @part table (cve_doc varchar(20), PARTIDAS_SURIDAS int) insert @part " +
                         "SELECT pd.CVE_DOC, ISNULL(COUNT(*), 0) PARTIDAS_SURTIDAS from @ped pd JOIN DETALLEPEDIDOMERC dm ON pd.cve_doc = dm.CVE_DOC " +
                         "where dm.CVE_ART <> '' and ISNULL(CANCELADO,0) = 0 group by pd.CVE_DOC " +
                         "declare @detped table (cve_doc varchar(20), ARTICULOS int, ARTICULOS_SURTIDOS int, PIEZAS int, PIEZAS_SURTIDAS int, PIEZAS_PENDIENTES int) " +
                         "insert @detped SELECT dp.CVE_DOC, COUNT(CVE_ART) AS ARTICULOS, SUM(CASE WHEN ISNULL(CANTPENDIENTE,0) <> ISNULL(CANT,0) THEN 1 ELSE 0 END) AS ARTICULOS_SURTIDOS, " +
                         "SUM(ISNULL(CANT, 0)) AS PIEZAS, SUM(ISNULL(CANTSURTIDO, 0)) AS PIEZAS_SURTIDAS, SUM(ISNULL(CANTPENDIENTE, 0)) AS PIEZAS_PENDIENTES " +
                         "FROM dbo.DETALLEPEDIDO dp join @ped pd on pd.cve_doc = dp.CVE_DOC GROUP BY dp.CVE_DOC ";

            List<EstadisticasGenerales> estGen = CargaEstGen();
            List<EstadisticasEstatus> estEst = CargaEstEst();
            List<EstadisticasEmpleado> estSurt = CargaEstSurt();
            List<EstadisticasEmpleado> estBroc = CargaEstBroc();
            List<EstadisticasEmpleado> estEmpq = CargaEstEmpq();
            List<EstadisticasEmpleado> estEtiq = CargaEstEtiq();
            List<EstadisticasPedidos> estHigh = CargaEstHigh();
            List<EstadisticasPedidos> estLow = CargaEstLow();

            ReportEstadisticas repEst = new ReportEstadisticas();
            repEst.DataSource = estGen;
            ReportEstadisticasEstatus repEstEst = new ReportEstadisticasEstatus();
            repEstEst.DataSource = estEst;
            ReportEstadisticasSurtidor repEstSurt = new ReportEstadisticasSurtidor();
            repEstSurt.DataSource = estSurt;
            ReportEstadisticasBrocas repEstBroc = new ReportEstadisticasBrocas();
            repEstBroc.DataSource = estBroc;
            ReportEstadisticasEmpacador repEstEmpq = new ReportEstadisticasEmpacador();
            repEstEmpq.DataSource = estEmpq;
            ReportEstadisticaEtiquetador repEstEtiq = new ReportEstadisticaEtiquetador();
            repEstEtiq.DataSource = estEtiq;
            ReportEstadisticoTenHigh repEstHigh = new ReportEstadisticoTenHigh();
            repEstHigh.DataSource = estHigh;
            ReportEstadisticoTenLow repEstLow = new ReportEstadisticoTenLow();
            repEstLow.DataSource = estLow;

            repEst.xrSubreport1.ReportSource = repEstEst;
            repEst.xrSubreport2.ReportSource = repEstSurt;
            repEst.xrSubreport3.ReportSource = repEstBroc;
            repEst.xrSubreport4.ReportSource = repEstEmpq;
            repEst.xrSubreport5.ReportSource = repEstEtiq;
            repEst.xrSubreport6.ReportSource = repEstHigh;
            repEst.xrSubreport7.ReportSource = repEstLow;

            repEst.ShowPreview();
        }

        private List<EstadisticasGenerales> CargaEstGen()
        {
            List<EstadisticasGenerales> dets = new List<EstadisticasGenerales>();
            try
            {
                var query = queryConst +
                            "select @fini FECHINICIAL, @ffin FECHFINAL,  count(*) PEDIDOS, @pedcan CANCELADOS, SUM(ARTICULOS) ARTICULOS, " +
                            "SUM(ARTICULOS_SURTIDOS) ARTICULOS_SURTIDOS, (CAST(SUM(ARTICULOS_SURTIDOS) AS FLOAT) / CAST(SUM(ARTICULOS) AS FLOAT)) PORC_ARTICULOS, " +
                            "SUM(PIEZAS) PIEZAS, SUM(PIEZAS_SURTIDAS) PIEZAS_SURTIDAS, (CAST(SUM(PIEZAS_SURTIDAS) AS FLOAT) / CAST(SUM(PIEZAS)AS FLOAT)) PORC_PIEZAS " +
                            "from @detped";
                dets = GetDataTable("DB", query, 1).ToList<EstadisticasGenerales>();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dets;
        }

        private List<EstadisticasEstatus> CargaEstEst()
        {
            List<EstadisticasEstatus> dets = new List<EstadisticasEstatus>();
            try
            {
                var query = queryConst +
                            "select e.ESTATUSPEDIDO, count(e.CVE_DOC) Cantidad, (count(e.CVE_DOC) / @dias) promedio  from PEDIDO p " +
                            "JOIN vw_estatuspedido e ON e.CVE_DOC = p.CVE_DOC JOIN @ped pd on pd.cve_doc = e.CVE_DOC group by e.ESTATUSPEDIDO";
                dets = GetDataTable("DB", query, 2).ToList<EstadisticasEstatus>();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dets;
        }

        private List<EstadisticasEmpleado> CargaEstSurt()
        {
            List<EstadisticasEmpleado> dets = new List<EstadisticasEmpleado>();
            try
            {
                var query = queryConst +
                            "delete @ped insert @ped select distinct cve_doc from PEDIDO_HIST ph " +
                            "WHERE ESTATUSPEDIDO IN('SURTIENDO', 'SURTIENDO BROCAS', 'EMPAQUE', 'DETENIDO') and FECHAMOV between @fini and @ffin + 1 " +
                            "delete @detped insert @detped SELECT dp.CVE_DOC, COUNT(CVE_ART) AS ARTICULOS, SUM(CASE WHEN ISNULL(CANTPENDIENTE,0) <> ISNULL(CANT,0) THEN 1 ELSE 0 END) AS ARTICULOS_SURTIDOS, " +
                            "SUM(ISNULL(CANT, 0)) AS PIEZAS, SUM(ISNULL(CANTSURTIDO, 0)) AS PIEZAS_SURTIDAS, SUM(ISNULL(CANTPENDIENTE, 0)) AS PIEZAS_PENDIENTES " +
                            "FROM dbo.DETALLEPEDIDO dp join @ped pd on pd.cve_doc = dp.CVE_DOC GROUP BY dp.CVE_DOC " +
                            "select p.SURTIDOR_ASIGNADO, u.Nombre, count(e.CVE_DOC) Pedidos, (count(e.CVE_DOC) / @dias) prom_PedidosDiario, " +
                            "sum(s.ARTICULOS_SURTIDOS) articulos, (sum(s.ARTICULOS_SURTIDOS) / @dias) prom_ArticulosDiario, " +
                            "sum(T.TIEMPO) tiempo, (sum(T.TIEMPO) / @dias) prom_TiempoDiario, " +
                            "(sum(T.TIEMPO) / sum(s.articulos)) prom_tiempoxArticulo, (sum(T.TIEMPO) / count(e.CVE_DOC)) prom_tiempoxPedido " +
                            "from PEDIDO p JOIN @ped pd ON pd.cve_doc = p.CVE_DOC JOIN vw_estatuspedido e ON e.CVE_DOC = p.CVE_DOC " +
                            "JOIN @detped s ON s.cve_doc = p.CVE_DOC JOIN USUARIOS u on u.Usuario = p.SURTIDOR_ASIGNADO " +
                            "JOIN( SELECT *, ISNULL(DATEDIFF(MINUTE, FECHASURTIR, ISNULL(FECHASURTIRBROCAS, FECHAEMPAQUE)), 0) - " +
                            "ISNULL(DATEDIFF(MINUTE, FECHADETENIDO, FECHARESURTIR), 0) TIEMPO FROM( " +
                            "select ph.CVE_DOC, MIN(case when ESTATUSPEDIDO = 'SURTIENDO' then FECHAMOV end) FECHASURTIR, " +
                            "MIN(case when ESTATUSPEDIDO = 'SURTIENDO BROCAS' then FECHAMOV end) FECHASURTIRBROCAS, " +
                            "MIN(case when ESTATUSPEDIDO = 'EMPAQUE' then FECHAMOV end) FECHAEMPAQUE, " +
                            "MIN(case when ESTATUSPEDIDO = 'DETENIDO' then FECHAMOV end) FECHADETENIDO, " +
                            "MAX(case when ESTATUSPEDIDO = 'SURTIENDO' then FECHAMOV end) FECHARESURTIR " +
                            "from PEDIDO_HIST ph join @ped pd on pd.cve_doc = ph.CVE_DOC " +
                            "WHERE ESTATUSPEDIDO IN('SURTIENDO', 'SURTIENDO BROCAS', 'EMPAQUE', 'DETENIDO') " +
                            "and FECHAMOV between @fini and @ffin + 1 GROUP BY ph.CVE_DOC) AS A " +
                            ") AS T ON T.CVE_DOC = P.CVE_DOC group by p.SURTIDOR_ASIGNADO, u.Nombre";
                dets = GetDataTable("DB", query, 3).ToList<EstadisticasEmpleado>();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dets;
        }

        private List<EstadisticasEmpleado> CargaEstBroc()
        {
            List<EstadisticasEmpleado> dets = new List<EstadisticasEmpleado>();
            try
            {
                var query = queryConst +
                            "delete @ped insert @ped select distinct cve_doc from PEDIDO_HIST ph " +
                            "WHERE ESTATUSPEDIDO IN('SURTIENDO', 'SURTIENDO BROCAS', 'EMPAQUE', 'DETENIDO BROCAS') and FECHAMOV between @fini and @ffin + 1 " +
                            "delete @detped insert @detped SELECT dp.CVE_DOC, COUNT(CVE_ART) AS ARTICULOS, SUM(CASE WHEN ISNULL(CANTPENDIENTE,0) <> ISNULL(CANT,0) THEN 1 ELSE 0 END) AS ARTICULOS_SURTIDOS, " +
                            "SUM(ISNULL(CANT, 0)) AS PIEZAS, SUM(ISNULL(CANTSURTIDO, 0)) AS PIEZAS_SURTIDAS, SUM(ISNULL(CANTPENDIENTE, 0)) AS PIEZAS_PENDIENTES " +
                            "FROM dbo.DETALLEPEDIDO dp join @ped pd on pd.cve_doc = dp.CVE_DOC GROUP BY dp.CVE_DOC " +
                            "select p.SURTIDOR_AREA, u.Nombre, count(e.CVE_DOC) Pedidos, (count(e.CVE_DOC) / @dias) prom_PedidosDiario, " +
                            "sum(s.ARTICULOS_SURTIDOS) articulos, (sum(s.ARTICULOS_SURTIDOS) / @dias) prom_ArticulosDiario, " +
                            "sum(T.TIEMPO) tiempo, (sum(T.TIEMPO) / @dias) prom_TiempoDiario, " +
                            "(sum(T.TIEMPO) / sum(s.articulos)) prom_tiempoxArticulo, (sum(T.TIEMPO) / count(e.CVE_DOC)) prom_tiempoxPedido " +
                            "from PEDIDO p JOIN @ped pd ON pd.cve_doc = p.CVE_DOC JOIN vw_estatuspedido e ON e.CVE_DOC = p.CVE_DOC " +
                            "JOIN @detped s ON s.cve_doc = p.CVE_DOC JOIN USUARIOS u on u.Usuario = p.SURTIDOR_AREA " +
                            "JOIN( SELECT *, ISNULL(DATEDIFF(MINUTE, FECHASURTIRBROCAS, FECHAEMPAQUE), 0) - " +
                            "ISNULL(DATEDIFF(MINUTE, FECHADETENIDO, FECHARESURTIR), 0) TIEMPO FROM( " +
                            "select ph.CVE_DOC, MIN(case when ESTATUSPEDIDO = 'SURTIENDO' then FECHAMOV end) FECHASURTIR," +
                            "MIN(case when ESTATUSPEDIDO = 'SURTIENDO BROCAS' then FECHAMOV end) FECHASURTIRBROCAS," +
                            "MIN(case when ESTATUSPEDIDO = 'EMPAQUE' then FECHAMOV end) FECHAEMPAQUE, " +
                            "MIN(case when ESTATUSPEDIDO = 'DETENIDO BROCAS' then FECHAMOV end) FECHADETENIDO, " +
                            "MAX(case when ESTATUSPEDIDO = 'SURTIENDO BROCAS' then FECHAMOV end) FECHARESURTIR " +
                            "from PEDIDO_HIST ph join @ped pd on pd.cve_doc = ph.CVE_DOC " +
                            "WHERE ESTATUSPEDIDO IN('SURTIENDO', 'SURTIENDO BROCAS', 'EMPAQUE', 'DETENIDO BROCAS') " +
                            "and FECHAMOV between @fini and @ffin + 1 GROUP BY ph.CVE_DOC) AS A " +
                            ") AS T ON T.CVE_DOC = P.CVE_DOC group by p.SURTIDOR_AREA, u.Nombre";
                dets = GetDataTable("DB", query, 4).ToList<EstadisticasEmpleado>();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dets;
        }

        private List<EstadisticasEmpleado> CargaEstEmpq()
        {
            List<EstadisticasEmpleado> dets = new List<EstadisticasEmpleado>();
            try
            {
                var query = queryConst +
                            "delete @ped insert @ped select distinct cve_doc from PEDIDO_HIST ph " +
                            "WHERE ESTATUSPEDIDO IN('EMPACANDO', 'REMISION', 'DETENIDO EMP') and FECHAMOV between @fini and @ffin + 1 " +
                            "delete @detped insert @detped SELECT dp.CVE_DOC, COUNT(CVE_ART) AS ARTICULOS, SUM(CASE WHEN ISNULL(CANTPENDIENTE,0) <> ISNULL(CANT,0) THEN 1 ELSE 0 END) AS ARTICULOS_SURTIDOS, " +
                            "SUM(ISNULL(CANT, 0)) AS PIEZAS, SUM(ISNULL(CANTSURTIDO, 0)) AS PIEZAS_SURTIDAS, SUM(ISNULL(CANTPENDIENTE, 0)) AS PIEZAS_PENDIENTES " +
                            "FROM dbo.DETALLEPEDIDO dp join @ped pd on pd.cve_doc = dp.CVE_DOC GROUP BY dp.CVE_DOC " +
                            "select p.EMPAQUETADOR_ASIGNADO, u.Nombre, count(e.CVE_DOC) Pedidos, (count(e.CVE_DOC) / @dias) prom_PedidosDiario, " +
                            "sum(s.ARTICULOS_SURTIDOS) articulos, (sum(s.ARTICULOS_SURTIDOS) / @dias) prom_ArticulosDiario, " +
                            "sum(T.TIEMPO) tiempo, (sum(T.TIEMPO) / @dias) prom_TiempoDiario, " +
                            "(sum(T.TIEMPO) / sum(s.articulos)) prom_tiempoxArticulo, (sum(T.TIEMPO) / count(e.CVE_DOC)) prom_tiempoxPedido " +
                            "from PEDIDO p JOIN @ped pd ON pd.cve_doc = p.CVE_DOC JOIN vw_estatuspedido e ON e.CVE_DOC = p.CVE_DOC " +
                            "JOIN @detped s ON s.cve_doc = p.CVE_DOC JOIN USUARIOS u on u.Usuario = p.EMPAQUETADOR_ASIGNADO " +
                            "JOIN( SELECT *, ISNULL(DATEDIFF(MINUTE, FECHAEMPACANDO, FECHAREMISION), 0) - " +
                            "ISNULL(DATEDIFF(MINUTE, FECHADETENIDO, FECHARESURTIR), 0) TIEMPO FROM( " +
                            "select ph.CVE_DOC, MIN(case when ESTATUSPEDIDO = 'EMPACANDO' then FECHAMOV end) FECHAEMPACANDO, " +
                            "MIN(case when ESTATUSPEDIDO = 'REMISION' then FECHAMOV end) FECHAREMISION, " +
                            "MIN(case when ESTATUSPEDIDO = 'DETENIDO EMP' then FECHAMOV end) FECHADETENIDO, " +
                            "MAX(case when ESTATUSPEDIDO = 'EMPACANDO' then FECHAMOV end) FECHARESURTIR " +
                            "from PEDIDO_HIST ph join @ped pd on pd.cve_doc = ph.CVE_DOC " +
                            "WHERE ESTATUSPEDIDO IN('EMPACANDO', 'REMISION', 'DETENIDO EMP') " +
                            "and FECHAMOV between @fini and @ffin + 1 GROUP BY ph.CVE_DOC) AS A " +
                            ") AS T ON T.CVE_DOC = P.CVE_DOC group by p.EMPAQUETADOR_ASIGNADO, u.Nombre";
                dets = GetDataTable("DB", query, 5).ToList<EstadisticasEmpleado>();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dets;
        }

        private List<EstadisticasEmpleado> CargaEstEtiq()
        {
            List<EstadisticasEmpleado> dets = new List<EstadisticasEmpleado>();
            try
            {
                var query = queryConst +
                            "delete @ped insert @ped select distinct cve_doc from PEDIDO_HIST ph " +
                            "WHERE ESTATUSPEDIDO IN('GUIA', 'TERMINADO', 'DETENIDO GUIA') and FECHAMOV between @fini and @ffin + 1 " +
                            "delete @detped insert @detped SELECT dp.CVE_DOC, COUNT(CVE_ART) AS ARTICULOS, SUM(CASE WHEN ISNULL(CANTPENDIENTE,0) <> ISNULL(CANT,0) THEN 1 ELSE 0 END) AS ARTICULOS_SURTIDOS, " +
                            "SUM(ISNULL(CANT, 0)) AS PIEZAS, SUM(ISNULL(CANTSURTIDO, 0)) AS PIEZAS_SURTIDAS, SUM(ISNULL(CANTPENDIENTE, 0)) AS PIEZAS_PENDIENTES " +
                            "FROM dbo.DETALLEPEDIDO dp join @ped pd on pd.cve_doc = dp.CVE_DOC GROUP BY dp.CVE_DOC " +
                            "select p.ETIQUETADOR_ASIGNADO, u.Nombre,  count(e.CVE_DOC) Pedidos, (count(e.CVE_DOC) / @dias) prom_PedidosDiario, " +
                            "sum(T.TIEMPO) tiempo, (sum(T.TIEMPO) / @dias) prom_TiempoDiario, (sum(T.TIEMPO) / count(e.CVE_DOC)) prom_tiempoxPedido " +
                            "from PEDIDO p JOIN @ped pd ON pd.cve_doc = p.CVE_DOC JOIN vw_estatuspedido e ON e.CVE_DOC = p.CVE_DOC " +
                            "JOIN @detped s ON s.cve_doc = p.CVE_DOC JOIN USUARIOS u on u.Usuario = p.ETIQUETADOR_ASIGNADO " +
                            "JOIN( SELECT *, ISNULL(DATEDIFF(MINUTE, FECHAGUIA, FECHATERMINADO), 0) - " +
                            "ISNULL(DATEDIFF(MINUTE, FECHADETENIDO, FECHARESURTIR), 0) TIEMPO FROM( " +
                            "select ph.CVE_DOC, MIN(case when ESTATUSPEDIDO = 'GUIA' then FECHAMOV end) FECHAGUIA, " +
                            "MIN(case when ESTATUSPEDIDO = 'TERMINADO' then FECHAMOV end) FECHATERMINADO, " +
                            "MIN(case when ESTATUSPEDIDO = 'DETENIDO EMP' then FECHAMOV end) FECHADETENIDO, " +
                            "MAX(case when ESTATUSPEDIDO = 'EMPACANDO' then FECHAMOV end) FECHARESURTIR " +
                            "from PEDIDO_HIST ph join @ped pd on pd.cve_doc = ph.CVE_DOC " +
                            "WHERE ESTATUSPEDIDO IN('GUIA', 'TERMINADO', 'DETENIDO GUIA') " +
                            "and FECHAMOV between @fini and @ffin + 1 GROUP BY ph.CVE_DOC) AS A " +
                            ") AS T ON T.CVE_DOC = P.CVE_DOC group by p.ETIQUETADOR_ASIGNADO, u.Nombre";
                dets = GetDataTable("DB", query, 6).ToList<EstadisticasEmpleado>();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dets;
        }

        private List<EstadisticasPedidos> CargaEstHigh()
        {
            List<EstadisticasPedidos> dets = new List<EstadisticasPedidos>();
            try
            {
                var query = queryConst +
                            "select distinct Top 10 p.CVE_CLPV, LTRIM(p.CVE_DOC) CVE_DOC, e.ESTATUSPEDIDO, c.NOMBRE Cliente, s.ARTICULOS, s.ARTICULOS_SURTIDOS, " +
                            "(s.ARTICULOS - s.ARTICULOS_SURTIDOS) ARTICULOS_PENDIENTES, s.PIEZAS, s.PIEZAS_SURTIDAS, s.PIEZAS_PENDIENTES, t.TIEMPO " +
                            "from PEDIDO p JOIN @ped pd ON pd.cve_doc = p.CVE_DOC JOIN vw_estatuspedido e ON e.CVE_DOC = p.CVE_DOC " +
                            "JOIN @detped s ON s.cve_doc = p.CVE_DOC JOIN CLIENTE c on c.CLAVE = p.CVE_CLPV JOIN( " +
                            "SELECT *, ISNULL(DATEDIFF(MINUTE, FECHAUTORIZACION, FECHATERMINADO), 0) TIEMPO FROM( " +
                            "select ph.CVE_DOC, MIN(case when ESTATUSPEDIDO = 'SURTIR' then FECHAMOV end) FECHAUTORIZACION, " +
                            "MIN(case when ESTATUSPEDIDO = 'FACTURACION' then FECHAMOV end) FECHATERMINADO from PEDIDO_HIST ph join @ped pd on pd.cve_doc = ph.CVE_DOC " +
                            "WHERE ESTATUSPEDIDO IN('SURTIR', 'FACTURACION') GROUP BY ph.CVE_DOC) AS A ) AS T ON T.CVE_DOC = P.CVE_DOC order by t.TIEMPO desc, LTRIM(p.CVE_DOC)";
                dets = GetDataTable("DB", query, 7).ToList<EstadisticasPedidos>();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dets;
        }

        private List<EstadisticasPedidos> CargaEstLow()
        {
            List<EstadisticasPedidos> dets = new List<EstadisticasPedidos>();
            try
            {
                var query = queryConst +
                            "select distinct Top 10 p.CVE_CLPV, LTRIM(p.CVE_DOC) CVE_DOC, e.ESTATUSPEDIDO, c.NOMBRE Cliente, s.ARTICULOS, s.ARTICULOS_SURTIDOS, " +
                            "(s.ARTICULOS - s.ARTICULOS_SURTIDOS) ARTICULOS_PENDIENTES, s.PIEZAS, s.PIEZAS_SURTIDAS, s.PIEZAS_PENDIENTES, t.TIEMPO " +
                            "from PEDIDO p JOIN @ped pd ON pd.cve_doc = p.CVE_DOC JOIN vw_estatuspedido e ON e.CVE_DOC = p.CVE_DOC " +
                            "JOIN @detped s ON s.cve_doc = p.CVE_DOC JOIN CLIENTE c on c.CLAVE = p.CVE_CLPV JOIN( " +
                            "SELECT *, ISNULL(DATEDIFF(MINUTE, FECHAUTORIZACION, FECHATERMINADO), 0) TIEMPO FROM( " +
                            "select ph.CVE_DOC, MIN(case when ESTATUSPEDIDO = 'SURTIR' then FECHAMOV end) FECHAUTORIZACION, " +
                            "MIN(case when ESTATUSPEDIDO = 'FACTURACION' then FECHAMOV end) FECHATERMINADO from PEDIDO_HIST ph join @ped pd on pd.cve_doc = ph.CVE_DOC " +
                            "WHERE ESTATUSPEDIDO IN('SURTIR', 'FACTURACION') GROUP BY ph.CVE_DOC) AS A ) AS T ON T.CVE_DOC = P.CVE_DOC order by t.TIEMPO, LTRIM(p.CVE_DOC)";
                dets = GetDataTable("DB", query, 7).ToList<EstadisticasPedidos>();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dets;
        }
    }
}