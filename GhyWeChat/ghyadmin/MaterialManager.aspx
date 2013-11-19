<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MaterialManager.aspx.cs" Inherits="GhyWeChat.ghyadmin.MaterialManager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>素材添加</title>
</head>
<body>
    <div class="navlist">
    <a class="addtextbutton">文字回复素材</a>
    <a class="addpicbutton">图片回复素材</a>
    <a class="addmusicbutton">音乐回复素材</a>
    </div>
    <div class="main">
    <div class="editorblock"></div>
    <input class="addbutton" type="button" value="添加" />
    <div class="show"></div>
    </div>
</body>
<script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $(".addtextbutton").css("cursor", "pointer");

        $(".addtextbutton").click(function () {
            if ($(".editorblock").html() != "") {
                return;
            }
            $(".editorblock").append("<textarea cols='60' rows='6' class='text'></textarea>");
            $(".addbutton").click(function () {
                var textdata = "{'text':'" + $(".text").val() + "'}";
                $.ajax({
                    url: "MaterialManager.aspx/AddText",
                    type: "post",
                    data: textdata,
                    datatype: "json",
                    contentType: "application/json; charset=utf-8",
                    beforeSend: function () {
                        $(".show").html("发送中");
                    },
                    success: function (data) {
                        $(".show").html("");
                        switch (data.d) {
                            case "1":
                                alert("成功");
                                break;
                            case "2":
                                alert("添加失败");
                                break;
                            case "3":
                                alert("该关键字已经存在");
                                break;
                            case "7":
                                alert("请重新登录");
                                self.location = "Login.aspx";
                                break;
                            default:
                                braek;
                        }
                    },
                    error: function (err) {
                        $(".show").html("错误" + err);
                    }
                });
            });

        });
    });
</script>
</html>
