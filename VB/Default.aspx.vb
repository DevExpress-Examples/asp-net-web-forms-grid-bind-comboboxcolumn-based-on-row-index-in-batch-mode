Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Collections.Specialized
Imports System.ComponentModel
Imports System.Linq
Imports DevExpress.Web.Data
Imports DevExpress.Web

Partial Public Class _Default
	Inherits System.Web.UI.Page
	Protected ReadOnly Property GridData() As List(Of GridDataItem)
		Get
			Dim key = "34FAA431-CF79-4869-9488-93F6AAE81263"
			If (Not IsPostBack) OrElse Session(key) Is Nothing Then
				Session(key) = Enumerable.Range(0, 100).Select(Function(i) New GridDataItem With {.ID = i, .C1 = i Mod 2, .C2 = i * 0.5 Mod 3, .C3 = "C3 " & i, .C4 = i Mod 2 = 0}).ToList()
			End If
			Return CType(Session(key), List(Of GridDataItem))
		End Get
	End Property
	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
		Grid.DataSource = GridData
		Grid.DataBind()
	End Sub
	Protected Sub Grid_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs)
		InsertNewItem(e.NewValues)
		CancelEditing(e)
	End Sub
	Protected Sub Grid_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs)
		UpdateItem(e.Keys, e.NewValues)
		CancelEditing(e)
	End Sub
	Protected Sub Grid_RowDeleting(ByVal sender As Object, ByVal e As ASPxDataDeletingEventArgs)
		DeleteItem(e.Keys, e.Values)
		CancelEditing(e)
	End Sub
	Protected Sub Grid_BatchUpdate(ByVal sender As Object, ByVal e As ASPxDataBatchUpdateEventArgs)
		If (Not BatchUpdateCheckBox.Checked) Then
			Return
		End If

		For Each args In e.InsertValues
			InsertNewItem(args.NewValues)
		Next args
		For Each args In e.UpdateValues
			UpdateItem(args.Keys, args.NewValues)
		Next args
		For Each args In e.DeleteValues
			DeleteItem(args.Keys, args.Values)
		Next args

		e.Handled = True
	End Sub
	Protected Function InsertNewItem(ByVal newValues As OrderedDictionary) As GridDataItem
		Dim item = New GridDataItem() With {.ID = GridData.Count}
		LoadNewValues(item, newValues)
		GridData.Add(item)
		Return item
	End Function
	Protected Function UpdateItem(ByVal keys As OrderedDictionary, ByVal newValues As OrderedDictionary) As GridDataItem
		Dim id = Convert.ToInt32(keys("ID"))
		Dim item = GridData.First(Function(i) i.ID = id)
		LoadNewValues(item, newValues)
		Return item
	End Function
	Protected Function DeleteItem(ByVal keys As OrderedDictionary, ByVal values As OrderedDictionary) As GridDataItem
		Dim id = Convert.ToInt32(keys("ID"))
		Dim item = GridData.First(Function(i) i.ID = id)
		GridData.Remove(item)
		Return item
	End Function
	Protected Sub LoadNewValues(ByVal item As GridDataItem, ByVal values As OrderedDictionary)
		item.C1 = Convert.ToInt32(values("C1"))
		item.C2 = Convert.ToDouble(values("C2"))
		item.C3 = Convert.ToString(values("C3"))
		item.C4 = Convert.ToBoolean(values("C4"))
	End Sub
	Protected Sub CancelEditing(ByVal e As CancelEventArgs)
		e.Cancel = True
		Grid.CancelEdit()
	End Sub
	Public Class GridDataItem
		Private privateID As Integer
		Public Property ID() As Integer
			Get
				Return privateID
			End Get
			Set(ByVal value As Integer)
				privateID = value
			End Set
		End Property
		Private privateC1 As Integer
		Public Property C1() As Integer
			Get
				Return privateC1
			End Get
			Set(ByVal value As Integer)
				privateC1 = value
			End Set
		End Property
		Private privateC2 As Double
		Public Property C2() As Double
			Get
				Return privateC2
			End Get
			Set(ByVal value As Double)
				privateC2 = value
			End Set
		End Property
		Private privateC3 As String
		Public Property C3() As String
			Get
				Return privateC3
			End Get
			Set(ByVal value As String)
				privateC3 = value
			End Set
		End Property
		Private privateC4 As Boolean
		Public Property C4() As Boolean
			Get
				Return privateC4
			End Get
			Set(ByVal value As Boolean)
				privateC4 = value
			End Set
		End Property
	End Class
	Protected Sub Grid_CellEditorInitialize(ByVal sender As Object, ByVal e As ASPxGridViewEditorEventArgs)
		If e.Column.FieldName = "C3" Then
			Dim combo As ASPxComboBox = TryCast(e.Editor, ASPxComboBox)
			AddHandler combo.Callback, AddressOf combo_Callback
		End If
	End Sub

	Private Sub combo_Callback(ByVal sender As Object, ByVal e As DevExpress.Web.CallbackEventArgsBase)
		Dim combo As ASPxComboBox = TryCast(sender, ASPxComboBox)
		For i As Integer = 0 To 9
			combo.Items.Add(String.Format("Row_{0} Item_{1}", e.Parameter, i))
		Next i
	End Sub
End Class