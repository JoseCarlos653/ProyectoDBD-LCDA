﻿Public Class FrmPrincipal

    'Instancia de un objeto de la clase Usuario como atributo del formulario principal
    Private _usuarioSistema As New Usuario()
    Public Property UsuarioSistema As Usuario
        Get
            Return _usuarioSistema
        End Get
        Set(value As Usuario)
            _usuarioSistema = value
        End Set
    End Property

    'Movimientos de Ventana.
    Dim ex As Integer, ey As Integer
    Dim Arrastre As Boolean
    Private Sub PanSuperior_MouseDown(sender As Object, e As MouseEventArgs) Handles PanSuperior.MouseDown
        ex = e.X
        ey = e.Y
        Arrastre = True
    End Sub

    Private Sub PanSuperior_MouseMove(sender As Object, e As MouseEventArgs) Handles PanSuperior.MouseMove
        If Arrastre Then
            Me.Location = Me.PointToScreen(New Point(MousePosition.X - Me.Location.X - ex, MousePosition.Y - Me.Location.Y - ey))
        End If
    End Sub

    Private Sub PanSuperior_MouseUp(sender As Object, e As MouseEventArgs) Handles PanSuperior.MouseUp
        Arrastre = False
    End Sub

    Private Sub FrmPrincipal_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Ajustar tamaño del formulario a la pantalla.
        Size = Screen.PrimaryScreen.WorkingArea.Size
        Location = Screen.PrimaryScreen.WorkingArea.Location
        cargarEtiquetas()
        cargarPermisos()

    End Sub

    'Cargar la información del usuario en el menú
    Public Sub cargarEtiquetas()
        'Asignar el nombre a los label con los atributos de la clase Usuario
        Lbl_nombreUser.Text = UsuarioSistema.NombreCompleto
        Lbl_rolUsuario.Text = UsuarioSistema.NombreRol
    End Sub

    'Función para desactivar los botones por defecto
    Public Sub desactivarBotones()
        BtnUsuarios.Enabled = False
        BtnRoles.Enabled = False
        BtnProductos.Enabled = False
        BtnMarca.Enabled = False
        BtnCategorias.Enabled = False
        BtnMovimientos.Enabled = False
        BtnReportes.Enabled = False
        BtnRepoProd.Enabled = False
        BtnRepoMov.Enabled = False
        BtnReporteMarcasProd.Enabled = False
        BtnRptCatgVend.Enabled = False
        BtnPreciosAltosBajos.Enabled = False
    End Sub

    'Función para cargar los permisos del usuario en base al rol
    Public Sub cargarPermisos()
        desactivarBotones()
        Dim resp As Boolean = False
        Try
            Dim permisos As List(Of Integer) = UsuarioSistema.ListaPermisos
            'Habilitar o deshabilitar botones de acuerdo a los permisos de los usuarios
            For Each per In permisos
                Select Case per
                    Case 1
                        BtnUsuarios.Enabled = True
                    Case 2
                        BtnRoles.Enabled = True
                    Case 3
                        BtnProductos.Enabled = True
                    Case 4
                        BtnMarca.Enabled = True
                    Case 5
                        BtnCategorias.Enabled = True
                    Case 6
                        BtnMovimientos.Enabled = True
                    Case 7
                        BtnReportes.Enabled = True
                        BtnRepoProd.Enabled = True
                        BtnRepoMov.Enabled = True
                        BtnReporteMarcasProd.Enabled = True
                        BtnRptCatgVend.Enabled = True
                        BtnPreciosAltosBajos.Enabled = True
                        BtnRepoProdSalidas.Enabled = True
                        BtnRepoProdEntradas.Enabled = True
                        BtnCantProdMarCat.Enabled = True
                End Select
            Next
            resp = True

        Catch ex As Exception

        End Try
    End Sub

    'Animaciones del menú.
    Private Sub TmrOcultar_Tick(sender As Object, e As EventArgs) Handles TmrOcultar.Tick
        If PanMenu.Width <= 60 Then
            TmrOcultar.Enabled = False
        Else
            PanMenu.Width = PanMenu.Width - 20
        End If
    End Sub

    Private Sub TmrMostrar_Tick(sender As Object, e As EventArgs) Handles TmrMostrar.Tick
        If PanMenu.Width >= 220 Then
            TmrMostrar.Enabled = False
        Else
            PanMenu.Width = PanMenu.Width + 20
        End If
    End Sub

    Private Sub PibMenu_Click(sender As Object, e As EventArgs) Handles PibMenu.Click
        If PanMenu.Width = 220 Then
            TmrOcultar.Enabled = True
        ElseIf PanMenu.Width = 60 Then
            TmrMostrar.Enabled = True
        End If
    End Sub

    Private Sub TmrHoraFecha_Tick(sender As Object, e As EventArgs) Handles TmrHoraFecha.Tick
        LblHora.Text = DateTime.Now.ToString("hh:mm")
        LblFecha.Text = DateTime.Now.ToShortDateString
    End Sub



    'Botones
    'Botón para cerrar el menú principal
    Private Sub PibCerrar_Click(sender As Object, e As EventArgs) Handles PibCerrar.Click
        Dim Respuesta = MsgBox("¿Estas seguro de que deseas salir del programa?", MsgBoxStyle.OkCancel, "Salir")
        If Respuesta = vbOK Then
            Application.Exit()
        End If
    End Sub

    'Botón para minimizar el formulario
    Private Sub PibMinimizar_Click(sender As Object, e As EventArgs) Handles PibMinimizar.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    'Botón para maximizar el formulario
    Private Sub PibMaximizar_Click(sender As Object, e As EventArgs) Handles PibMaximizar.Click
        Me.WindowState = FormWindowState.Maximized
    End Sub

    'Botón para mostrar el formulario de usuarios
    Private Sub BtnUsuarios_Click(sender As Object, e As EventArgs) Handles BtnUsuarios.Click
        FrmUsuarios.UsuarioSistema = UsuarioSistema
        FrmUsuarios.Show()
    End Sub

    'Botón para mostrar el formulario de productos
    Private Sub BtnProductos_Click(sender As Object, e As EventArgs) Handles BtnProductos.Click
        FrmProductos.Show()
    End Sub

    'Botón para mostrar el formulario de reportes
    Private Sub BtnReportes_Click(sender As Object, e As EventArgs) Handles BtnReportes.Click

        If PanReportes.Visible = False Then
            PanReportes.Visible = True
        Else
            PanReportes.Visible = False
        End If

    End Sub

    'Botón para mostrar el formulario de categorias
    Private Sub BtnCategorias_Click(sender As Object, e As EventArgs) Handles BtnCategorias.Click
        FrmCategoria.Show()
    End Sub

    'Botón para mostrar el formulario de roles
    Private Sub BtnRoles_Click(sender As Object, e As EventArgs) Handles BtnRoles.Click
        FrmRoles.UsuarioSistema = UsuarioSistema
        FrmRoles.Show()
    End Sub

    'Botón para mostrar el formulario de marcas
    Private Sub BtnMarca_Click(sender As Object, e As EventArgs) Handles BtnMarca.Click
        FrmMarcas.Show()
    End Sub

    'Botón para mostrar el formulario de movimientos
    Private Sub BtnMovimientos_Click(sender As Object, e As EventArgs) Handles BtnMovimientos.Click
        FrmMovimiento.UsuarioSistema = UsuarioSistema
        FrmMovimiento.Show()
    End Sub

    'Botón para mostrar el la informacion del usuario
    Private Sub PictureBox2_MouseClick(sender As Object, e As MouseEventArgs) Handles PictureBox2.MouseClick
        MsgBox("Nombre Usuario: " & UsuarioSistema.NombreCompleto & " | Rol del Usuario: " & UsuarioSistema.NombreRol, MsgBoxStyle.Information, "Principal")
    End Sub

    Private Sub BtnRepoProd_Click(sender As Object, e As EventArgs) Handles BtnRepoProd.Click
        Dim tbl As New DataTable
        Dim reporte As New DBLaCasaDelArteDataSetTableAdapters.RptProductosTableAdapter

        tbl = reporte.GetData
        VerReporte(tbl, "DataSet1", "rptProductos.rdlc")
    End Sub

    Private Sub BtnRepoMov_Click(sender As Object, e As EventArgs) Handles BtnRepoMov.Click
        Dim tbl As New DataTable
        Dim reporte As New DBLaCasaDelArteDataSetTableAdapters.RptMovimientosTableAdapter

        tbl = reporte.GetData
        VerReporte(tbl, "DataSet1", "rptMovimientos.rdlc")
    End Sub

    Private Sub BtnReporteMarcasProd_Click(sender As Object, e As EventArgs) Handles BtnReporteMarcasProd.Click
        Dim tbl As New DataTable
        Dim reporte As New DBLaCasaDelArteDataSetTableAdapters.RptMarcasXProductoTableAdapter

        tbl = reporte.GetData
        VerReporte(tbl, "DataSet1", "rptMarcasXProducto.rdlc")
    End Sub

    Private Sub BtnPreciosAltosBajos_Click(sender As Object, e As EventArgs) Handles BtnPreciosAltosBajos.Click
        Dim tbl As New DataTable
        Dim reporte As New DBLaCasaDelArteDataSetTableAdapters.RptProductosXPrecioTableAdapter

        tbl = reporte.GetData
        VerReporte(tbl, "DataSet1", "rptPreciosAltosABajos.rdlc")
    End Sub

    Private Sub BtnRptCatgVend_Click(sender As Object, e As EventArgs) Handles BtnRptCatgVend.Click
        Dim tbl As New DataTable
        Dim reporte As New DBLaCasaDelArteDataSetTableAdapters.RptCategoriasXProductoTableAdapter

        tbl = reporte.GetData
        VerReporte(tbl, "DataSet1", "rptCategoriasXProducto.rdlc")
    End Sub

    Private Sub BtnRepoProdSalidas_Click(sender As Object, e As EventArgs) Handles BtnRepoProdSalidas.Click
        Dim tbl As New DataTable
        Dim reporte As New DBLaCasaDelArteDataSetTableAdapters.RptProductosMasSalidasTableAdapter

        tbl = reporte.GetData
        VerReporte(tbl, "DataSet1", "rptProductosMasSalidas.rdlc")
    End Sub

    Private Sub BtnRepoProdEntradas_Click(sender As Object, e As EventArgs) Handles BtnRepoProdEntradas.Click
        Dim tbl As New DataTable
        Dim reporte As New DBLaCasaDelArteDataSetTableAdapters.RptProductosMasEntradasTableAdapter

        tbl = reporte.GetData
        VerReporte(tbl, "DataSet1", "rptProductosMasEntradas.rdlc")
    End Sub

    Private Sub BtnCantProdMarCat_Click(sender As Object, e As EventArgs) Handles BtnCantProdMarCat.Click
        Dim tbl As New DataTable
        Dim reporte As New DBLaCasaDelArteDataSetTableAdapters.RptCantProdXMarcaCatTableAdapter

        tbl = reporte.GetData
        VerReporte(tbl, "DataSet1", "rptCantProdMarcaCat.rdlc")
    End Sub

    Private Sub BtnCerrarSesion_Click(sender As Object, e As EventArgs) Handles BtnCerrarSesion.Click
        Dim Respuesta = MsgBox("¿Estas seguro que deseas cerrar sesión?", MsgBoxStyle.OkCancel, "Cerrar Sesión")
        If Respuesta = vbOK Then
            Me.Close()
            FrmLogin.Show()
        End If
    End Sub


End Class
