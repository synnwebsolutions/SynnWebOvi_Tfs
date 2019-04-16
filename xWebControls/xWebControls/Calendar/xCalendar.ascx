<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="xCalendar.ascx.cs" Inherits="xWebControls.Calendar.xCalendar" %>

<style>
    
.dayitem {
    padding:10%;
    list-style-type: square;
    margin:auto;
      background-color:#808080;

}

.dayitemcontainer {
    padding:10%;
       border:none;
      background-color:#2B2B2D;
          width:100%;
}

.dayitemheader {
          background-color:black;
              padding:5%;
        color:white;
}

.maincontainer {
    width:100%;
    background-color:black;
   
}

.mainheaders {
         /*background-color:lightgrey;*/
         color:lightgray;
        
          width:14.3%;
          text-align:center;
}
.today{
        color:lightgray;
        background-color:tomato;
}
</style>
<div id="tbl" runat="server" >

</div>
