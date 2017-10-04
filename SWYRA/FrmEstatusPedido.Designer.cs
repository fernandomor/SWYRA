﻿namespace SWYRA
{
    partial class FrmEstatusPedido
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEstatusPedido));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.gpoFiltro = new System.Windows.Forms.GroupBox();
            this.chkActual = new DevExpress.XtraEditors.CheckEdit();
            this.txtFechFin = new DevExpress.XtraEditors.DateEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFechIni = new DevExpress.XtraEditors.DateEdit();
            this.Label1 = new System.Windows.Forms.Label();
            this.cbEstatusPed = new DevExpress.XtraEditors.ComboBoxEdit();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.pedidosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colcve_doc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcve_pedi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colfecha_doc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colfecha_cancela = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcondicion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coltiposervicio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colestatuspedido = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colocurredomicilio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colporc_surtido = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colporc_empaque = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colindicaciones = new DevExpress.XtraGrid.Columns.GridColumn();
            this.collote = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcliente = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcobrador_asignado_n = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcobrador_autorizo_n = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsurtidor_asignado_n = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colempaquetador_asignado_n = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coletiquetador_asignado_n = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsurtidor_area_n = new DevExpress.XtraGrid.Columns.GridColumn();
            this.toolStrip1.SuspendLayout();
            this.gpoFiltro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkActual.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFechFin.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFechFin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFechIni.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFechIni.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbEstatusPed.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pedidosBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSalir});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1064, 27);
            this.toolStrip1.TabIndex = 40;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnSalir
            // 
            this.btnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.Image = global::SWYRA.Properties.Resources.Logout_32x32;
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(60, 24);
            this.btnSalir.Text = "Salir";
            // 
            // gpoFiltro
            // 
            this.gpoFiltro.Controls.Add(this.chkActual);
            this.gpoFiltro.Controls.Add(this.txtFechFin);
            this.gpoFiltro.Controls.Add(this.label3);
            this.gpoFiltro.Controls.Add(this.label2);
            this.gpoFiltro.Controls.Add(this.txtFechIni);
            this.gpoFiltro.Controls.Add(this.Label1);
            this.gpoFiltro.Controls.Add(this.cbEstatusPed);
            this.gpoFiltro.Location = new System.Drawing.Point(12, 30);
            this.gpoFiltro.Name = "gpoFiltro";
            this.gpoFiltro.Size = new System.Drawing.Size(1040, 65);
            this.gpoFiltro.TabIndex = 41;
            this.gpoFiltro.TabStop = false;
            this.gpoFiltro.Text = " Filtrar Pedidos ";
            // 
            // chkActual
            // 
            this.chkActual.EditValue = true;
            this.chkActual.Location = new System.Drawing.Point(864, 27);
            this.chkActual.Name = "chkActual";
            this.chkActual.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkActual.Properties.Appearance.Options.UseFont = true;
            this.chkActual.Properties.Caption = "DATOS ACTUALES";
            this.chkActual.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Style1;
            this.chkActual.Size = new System.Drawing.Size(165, 22);
            this.chkActual.TabIndex = 43;
            // 
            // txtFechFin
            // 
            this.txtFechFin.EditValue = null;
            this.txtFechFin.Location = new System.Drawing.Point(686, 26);
            this.txtFechFin.Name = "txtFechFin";
            this.txtFechFin.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFechFin.Properties.Appearance.Options.UseFont = true;
            this.txtFechFin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtFechFin.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtFechFin.Size = new System.Drawing.Size(147, 24);
            this.txtFechFin.TabIndex = 42;
            this.txtFechFin.TextChanged += new System.EventHandler(this.txtFechFin_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(660, 29);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 18);
            this.label3.TabIndex = 5;
            this.label3.Text = "al";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(339, 32);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "Fecha Elaboración";
            // 
            // txtFechIni
            // 
            this.txtFechIni.EditValue = null;
            this.txtFechIni.Location = new System.Drawing.Point(506, 26);
            this.txtFechIni.Name = "txtFechIni";
            this.txtFechIni.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFechIni.Properties.Appearance.Options.UseFont = true;
            this.txtFechIni.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtFechIni.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtFechIni.Size = new System.Drawing.Size(147, 24);
            this.txtFechIni.TabIndex = 3;
            this.txtFechIni.TextChanged += new System.EventHandler(this.txtFechIni_TextChanged);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(17, 29);
            this.Label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(108, 18);
            this.Label1.TabIndex = 2;
            this.Label1.Text = "Estatus Pedido";
            // 
            // cbEstatusPed
            // 
            this.cbEstatusPed.Location = new System.Drawing.Point(141, 26);
            this.cbEstatusPed.Name = "cbEstatusPed";
            this.cbEstatusPed.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbEstatusPed.Properties.Appearance.Options.UseFont = true;
            this.cbEstatusPed.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbEstatusPed.Properties.Items.AddRange(new object[] {
            "AUTORIZACION",
            "SURTIR",
            "DETENIDO",
            "EMPAQUE",
            "REMISION",
            "LEVANTAMIENTO DE GUIAS",
            "MODIFICACION",
            "CANCELACION",
            "TERMINADO"});
            this.cbEstatusPed.Size = new System.Drawing.Size(171, 24);
            this.cbEstatusPed.TabIndex = 0;
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.pedidosBindingSource;
            this.gridControl1.Location = new System.Drawing.Point(12, 113);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1040, 346);
            this.gridControl1.TabIndex = 42;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // pedidosBindingSource
            // 
            this.pedidosBindingSource.DataSource = typeof(SWYRA.Pedidos);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colcve_doc,
            this.colcliente,
            this.colcve_pedi,
            this.colfecha_doc,
            this.colfecha_cancela,
            this.colcondicion,
            this.coltiposervicio,
            this.colestatuspedido,
            this.colocurredomicilio,
            this.colcobrador_asignado_n,
            this.colcobrador_autorizo_n,
            this.colsurtidor_asignado_n,
            this.colempaquetador_asignado_n,
            this.coletiquetador_asignado_n,
            this.colsurtidor_area_n,
            this.colporc_surtido,
            this.colporc_empaque,
            this.colindicaciones,
            this.collote});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsMenu.EnableFooterMenu = false;
            this.gridView1.OptionsMenu.EnableGroupPanelMenu = false;
            // 
            // colcve_doc
            // 
            this.colcve_doc.Caption = "Orden ID";
            this.colcve_doc.FieldName = "cve_doc";
            this.colcve_doc.Name = "colcve_doc";
            this.colcve_doc.Visible = true;
            this.colcve_doc.VisibleIndex = 0;
            // 
            // colcve_pedi
            // 
            this.colcve_pedi.Caption = "Clave Pedido";
            this.colcve_pedi.FieldName = "cve_pedi";
            this.colcve_pedi.Name = "colcve_pedi";
            this.colcve_pedi.Visible = true;
            this.colcve_pedi.VisibleIndex = 1;
            // 
            // colfecha_doc
            // 
            this.colfecha_doc.Caption = "Fecha";
            this.colfecha_doc.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.colfecha_doc.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colfecha_doc.FieldName = "fecha_doc";
            this.colfecha_doc.Name = "colfecha_doc";
            this.colfecha_doc.Visible = true;
            this.colfecha_doc.VisibleIndex = 2;
            // 
            // colfecha_cancela
            // 
            this.colfecha_cancela.Caption = "Fecha Cancelado";
            this.colfecha_cancela.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.colfecha_cancela.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colfecha_cancela.FieldName = "fecha_cancela";
            this.colfecha_cancela.Name = "colfecha_cancela";
            this.colfecha_cancela.Visible = true;
            this.colfecha_cancela.VisibleIndex = 3;
            // 
            // colcondicion
            // 
            this.colcondicion.Caption = "Condición";
            this.colcondicion.FieldName = "condicion";
            this.colcondicion.Name = "colcondicion";
            this.colcondicion.Visible = true;
            this.colcondicion.VisibleIndex = 4;
            // 
            // coltiposervicio
            // 
            this.coltiposervicio.Caption = "Tipo Servicio";
            this.coltiposervicio.FieldName = "tiposervicio";
            this.coltiposervicio.Name = "coltiposervicio";
            this.coltiposervicio.Visible = true;
            this.coltiposervicio.VisibleIndex = 5;
            // 
            // colestatuspedido
            // 
            this.colestatuspedido.Caption = "Estatus Pedido";
            this.colestatuspedido.FieldName = "estatuspedido";
            this.colestatuspedido.Name = "colestatuspedido";
            this.colestatuspedido.Visible = true;
            this.colestatuspedido.VisibleIndex = 6;
            // 
            // colocurredomicilio
            // 
            this.colocurredomicilio.Caption = "Ocurre a Domicilio";
            this.colocurredomicilio.FieldName = "ocurredomicilio";
            this.colocurredomicilio.Name = "colocurredomicilio";
            this.colocurredomicilio.Visible = true;
            this.colocurredomicilio.VisibleIndex = 7;
            // 
            // colporc_surtido
            // 
            this.colporc_surtido.Caption = "Porcentaje Surtido";
            this.colporc_surtido.FieldName = "porc_surtido";
            this.colporc_surtido.Name = "colporc_surtido";
            this.colporc_surtido.Visible = true;
            this.colporc_surtido.VisibleIndex = 8;
            // 
            // colporc_empaque
            // 
            this.colporc_empaque.Caption = "Porcentaje Empaque";
            this.colporc_empaque.FieldName = "porc_empaque";
            this.colporc_empaque.Name = "colporc_empaque";
            this.colporc_empaque.Visible = true;
            this.colporc_empaque.VisibleIndex = 9;
            // 
            // colindicaciones
            // 
            this.colindicaciones.Caption = "Indicaciones";
            this.colindicaciones.FieldName = "indicaciones";
            this.colindicaciones.Name = "colindicaciones";
            this.colindicaciones.Visible = true;
            this.colindicaciones.VisibleIndex = 10;
            // 
            // collote
            // 
            this.collote.Caption = "Lote";
            this.collote.FieldName = "lote";
            this.collote.Name = "collote";
            this.collote.Visible = true;
            this.collote.VisibleIndex = 11;
            // 
            // colcliente
            // 
            this.colcliente.Caption = "Cliente";
            this.colcliente.FieldName = "cliente";
            this.colcliente.Name = "colcliente";
            this.colcliente.Visible = true;
            this.colcliente.VisibleIndex = 12;
            // 
            // colcobrador_asignado_n
            // 
            this.colcobrador_asignado_n.Caption = "Cobrador Asignado";
            this.colcobrador_asignado_n.FieldName = "cobrador_asignado_n";
            this.colcobrador_asignado_n.Name = "colcobrador_asignado_n";
            this.colcobrador_asignado_n.Visible = true;
            this.colcobrador_asignado_n.VisibleIndex = 13;
            // 
            // colcobrador_autorizo_n
            // 
            this.colcobrador_autorizo_n.Caption = "Cobrador Autorizo";
            this.colcobrador_autorizo_n.CustomizationCaption = "a";
            this.colcobrador_autorizo_n.FieldName = "cobrador_autorizo_n";
            this.colcobrador_autorizo_n.Name = "colcobrador_autorizo_n";
            this.colcobrador_autorizo_n.Visible = true;
            this.colcobrador_autorizo_n.VisibleIndex = 14;
            // 
            // colsurtidor_asignado_n
            // 
            this.colsurtidor_asignado_n.Caption = "Surtidor Asignado";
            this.colsurtidor_asignado_n.FieldName = "surtidor_asignado_n";
            this.colsurtidor_asignado_n.Name = "colsurtidor_asignado_n";
            this.colsurtidor_asignado_n.Visible = true;
            this.colsurtidor_asignado_n.VisibleIndex = 15;
            // 
            // colempaquetador_asignado_n
            // 
            this.colempaquetador_asignado_n.Caption = "Empaquetador Asignado";
            this.colempaquetador_asignado_n.FieldName = "empaquetador_asignado_n";
            this.colempaquetador_asignado_n.Name = "colempaquetador_asignado_n";
            this.colempaquetador_asignado_n.Visible = true;
            this.colempaquetador_asignado_n.VisibleIndex = 16;
            // 
            // coletiquetador_asignado_n
            // 
            this.coletiquetador_asignado_n.Caption = "Etiquetador Asignado";
            this.coletiquetador_asignado_n.FieldName = "etiquetador_asignado_n";
            this.coletiquetador_asignado_n.Name = "coletiquetador_asignado_n";
            this.coletiquetador_asignado_n.Visible = true;
            this.coletiquetador_asignado_n.VisibleIndex = 17;
            // 
            // colsurtidor_area_n
            // 
            this.colsurtidor_area_n.Caption = "Surtidor Área";
            this.colsurtidor_area_n.FieldName = "surtidor_area_n";
            this.colsurtidor_area_n.Name = "colsurtidor_area_n";
            this.colsurtidor_area_n.Visible = true;
            this.colsurtidor_area_n.VisibleIndex = 18;
            // 
            // FrmEstatusPedido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 471);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.gpoFiltro);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmEstatusPedido";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ESTATUS PEDIDO";
            this.Load += new System.EventHandler(this.FrmEstatusPedido_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.gpoFiltro.ResumeLayout(false);
            this.gpoFiltro.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkActual.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFechFin.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFechFin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFechIni.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFechIni.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbEstatusPed.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pedidosBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private System.Windows.Forms.GroupBox gpoFiltro;
        private DevExpress.XtraEditors.ComboBoxEdit cbEstatusPed;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.DateEdit txtFechIni;
        private DevExpress.XtraEditors.DateEdit txtFechFin;
        internal System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.CheckEdit chkActual;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.BindingSource pedidosBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colcve_doc;
        private DevExpress.XtraGrid.Columns.GridColumn colcliente;
        private DevExpress.XtraGrid.Columns.GridColumn colcve_pedi;
        private DevExpress.XtraGrid.Columns.GridColumn colfecha_doc;
        private DevExpress.XtraGrid.Columns.GridColumn colfecha_cancela;
        private DevExpress.XtraGrid.Columns.GridColumn colcondicion;
        private DevExpress.XtraGrid.Columns.GridColumn coltiposervicio;
        private DevExpress.XtraGrid.Columns.GridColumn colestatuspedido;
        private DevExpress.XtraGrid.Columns.GridColumn colocurredomicilio;
        private DevExpress.XtraGrid.Columns.GridColumn colcobrador_asignado_n;
        private DevExpress.XtraGrid.Columns.GridColumn colcobrador_autorizo_n;
        private DevExpress.XtraGrid.Columns.GridColumn colsurtidor_asignado_n;
        private DevExpress.XtraGrid.Columns.GridColumn colempaquetador_asignado_n;
        private DevExpress.XtraGrid.Columns.GridColumn coletiquetador_asignado_n;
        private DevExpress.XtraGrid.Columns.GridColumn colsurtidor_area_n;
        private DevExpress.XtraGrid.Columns.GridColumn colporc_surtido;
        private DevExpress.XtraGrid.Columns.GridColumn colporc_empaque;
        private DevExpress.XtraGrid.Columns.GridColumn colindicaciones;
        private DevExpress.XtraGrid.Columns.GridColumn collote;
    }
}