<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddTextResponse.aspx.cs" Inherits="GhyWeChat.ghyadmin.AddTextResponse" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
   
    <div>
    关键字<input class="key" type="text"  /><br />
    内容中不要包括 " ' * 之类的字符"
    回复内容<textarea class="text" cols="50" rows="5" style="width:300px;">
    </textarea><br />
    <input class="AddButton" type="button" value="添加" />
    </div>
    <script src="Scripts/Adam.js" type="text/javascript"></script>
    <script type="text/javascript">
        loadScript("Scripts/jquery-1.4.1.js", function () { add(); });
    function add() {
        $(".AddButton").click(function () {
            var getdata = "{'key':'" + $(".key").val() + "','text':'" + $(".text").val() + "','msgType':'1'}"
            $.ajax({
                type: "Post",
                url: "AddTextResponse.aspx/AddText",
                data: getdata,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $(".show").html("加载中");
                },
                success: function (data) {
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
                        case "4":
                            alert("关键字不能为空");
                            break;
                        case "5":
                            alert("内容不能为空");
                            break;
                        case "6":
                            alert("内部错误,请通知程序员");
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
    }
    </script>
</body>
</html>
