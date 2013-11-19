<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditorNotice.aspx.cs" Inherits="GhyWeChat.ghyadmin.EditorNotice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <input class="editbutton" type="button" value="修改"  />
    <div class="show"></div>
</body>
<script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
<script type="text/javascript">

    $(document).ready(function () {
        $(".editbutton").click(function () {
            var getdata = "{'text':'" + $(".introducetext") + "'}";
            $.ajax({
                type: "Post",
                url: "EditorIntroduce.aspx/Editor",
                data: getdata,
                datatype: "json",
                contentType: "application/json; charset=utf-8",
                beforeSend: function () {
                    $(".show").html("加载中");
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
    });
</script>
</html>
