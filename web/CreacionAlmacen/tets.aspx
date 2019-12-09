<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tets.aspx.cs" Inherits="JQuery.tets" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Test page</title>
    <script src="bootstrap/js/jquery-1.9.1.min.js" type="text/javascript"></script>
    <script src="bootstrap/js/bootstrap-tooltip.js" type="text/javascript"></script>
    <script src="bootstrap/js/bootstrap-popover.js" type="text/javascript"></script>
    <script src="bootstrap/jsFunctions/createGrid.js" type="text/javascript"></script>

    <link href="bootstrap/css/bootstrap.css" rel="Stylesheet" media="screen" />
    <link href="bootstrap/css/bootstrap-responsive.css" rel="Stylesheet" media="screen" />
    <style type="text/css">
    .bs-docs-example {
        position: relative;
        margin: 15px 0;
        padding: 39px 19px 14px;
        background-color: #fff;
        border: 1px solid #ddd;
        -webkit-border-radius: 4px;
        -moz-border-radius: 4px;
        border-radius: 4px;
    }
    .bs-docs-example:after {
        content: "Example";
        position: absolute;
        top: -1px;
        left: -1px;
        padding: 3px 7px;
        font-size: 12px;
        font-weight: bold;
        background-color: #f5f5f5;
        border: 1px solid #ddd;
        color: #9da0a4;
        -webkit-border-radius: 4px 0 4px 0;
        -moz-border-radius: 4px 0 4px 0;
        border-radius: 4px 0 4px 0;
    }
    body
    {
        font-family: Verdana;
        font-size: 11px;  
    }
    
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            var $msg = $(".alert");
            $("input#click1").click(function () {
                var d = new Date();
                                    
                var sendD = (d.getMonth() + 1) + ' ' + d.toString("yyyy-MM-dd hh:mm:ss").substring(7, 24);
                addMsg(sendD);
            });
        });


        function addMsg(d) {
            var $div = $(".messages");
            var msg = "hi " + d; 
            var msg_container = 
                "<div class='alert' style='margin-bottom: 5px;'>" +
                    "<button type='button' class='close' data-dismiss='alert'>×</button>" +
                    "<strong>Alert!</strong>" + msg +
                "</div>";

            $(msg_container).hide().appendTo($div).show('slow');
            //$div.append(msg_container);
            //$(".alert").show('slow');
    
        }

    </script>
</head>
<body>

<div class="container">
<br />

    <input type="button" value="click1" id="click1" class="btn btn-small btn-danger" />

    <div class="bs-docs-example">
        <div class="messages">
            <div class="alert">
            <button type="button" class="close" data-dismiss="alert">×</button>
            <strong>Alert!</strong> holaaa!
            </div>
        </div>    
    </div>

     
    <!-- msg -->
    
    

</div>





    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>


    <script src="bootstrap/js/bootstrap-transition.js"></script>
    <script src="bootstrap/js/bootstrap-alert.js"></script>
    <script src="bootstrap/js/bootstrap-modal.js"></script>
    <script src="bootstrap/js/bootstrap-dropdown.js"></script>
    <script src="bootstrap/js/bootstrap-scrollspy.js"></script>
    <script src="bootstrap/js/bootstrap-tab.js"></script>
    <script src="bootstrap/js/bootstrap-tooltip.js"></script>
    <script src="bootstrap/js/bootstrap-popover.js"></script>
    <%--<script src="bootstrap/js/bootstrap-button.js"></script>--%>
    <script src="bootstrap/js/bootstrap-collapse.js"></script>
    <script src="bootstrap/js/bootstrap-carousel.js"></script>
    <script src="bootstrap/js/bootstrap-typeahead.js"></script>
    <script src="bootstrap/js/bootstrap-affix.js"></script>

</body>
</html>
