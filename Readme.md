<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128533398/14.1.8%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T190978)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* **[Default.aspx](./CS/Default.aspx) (VB: [Default.aspx](./VB/Default.aspx))**
* [Default.aspx.cs](./CS/Default.aspx.cs) (VB: [Default.aspx.vb](./VB/Default.aspx.vb))
<!-- default file list end -->
# ASPxGridView - How to bind a combobox column based on a row index in Batch Edit mode
<!-- run online -->
**[[Run Online]](https://codecentral.devexpress.com/128533398/)**
<!-- run online end -->


<p>The attached example illustrates how to populate GridViewDataComboBoxColumn at runtime when ASPxGridView is used in Batch Edit mode.</p>
<p>The technique of completing this task in a batch grid differs from the approach used for a grid in standard mode. When the grid is used in Batch edit mode, ASPxGridView performs all operations on the client side. Therefore, it's not possible to populate the combo box located in each row using the <a href="https://documentation.devexpress.com/#AspNet/DevExpressWebASPxGridView_CellEditorInitializetopic">CellEditorInitialize</a> event handler.<br />As a solution, the combo box can be populated on its callback.<br />This scenario requires the following steps:<br />1) Add the combo box' Callback event handler in the grid's <a href="https://documentation.devexpress.com/#AspNet/DevExpressWebASPxGridView_CellEditorInitializetopic">CellEditorInitialize</a> event handler.<br />2) Initiate a callback to the combo box by calling its PerformCallback method in the client-side <a href="https://documentation.devexpress.com/AspNet/DevExpressWebASPxGridViewScriptsASPxClientGridView_BatchEditStartEditingtopic.aspx">ASPxClientGridView.BatchEditStartEditing</a> event handler. Pass a row's visible index obtained via e.visibleIndex as a parameter of this method.<br />3) Add required items to the combo box in its server-side Callback event handler. You can get the current row index from the callback parameter (e.Parameter).</p>

<br/>


