<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="GhyWeChat._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GHYWeChat</title> 
</head>
<body>
  
    <input type="text" class="username" /><br />
    <input type="password" class="password" /><br />
    <input type="button" class="loginbutton" value="登录" /><br />
    <div class="show"></div>
   
</body>
<script src="Scripts/Adam.js" type="text/javascript"></script>
<script type="text/javascript">
    loadScript("Scripts/jquery-1.4.1.js", function () {  Login(); });
    function Login() {
        $(".loginbutton").click(function () {
            var username = $(".username").val();
            var password = $(".password").val();
            var getdata = "{'username':'" + username + "','password':'" + password + "'}";
            $.ajax({
                type: "Post",
                url: "Login.aspx/Login",
                data: getdata,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $(".show").html("加载中");
                },
                success: function (data) {
                    switch (data.d) {
                        case "1":
                            self.location = "Default.aspx";
                            break;
                        case "2":
                            alert("登录失败");
                            break;
                        default:
                    }
                },
                error: function (err) {
                    $(".show").html("错误" + err);
                }
            });
        });
    }
</script>
</html>
