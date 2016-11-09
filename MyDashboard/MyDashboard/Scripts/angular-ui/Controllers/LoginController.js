(function () {
    "use strict";

    angular
        .module("Dashboard.controllers.LoginController",[])
        .controller("LoginController", LoginController);

    function LoginController($scope, $compile) {
        var vm = this;
        vm.username = null;
        vm.password = null;
        vm.url = null;
        vm.init = function (url){
            if(url != null)
                vm.url = url;
        }
        vm.logIn = function(){
            var key = CryptoJS.enc.Utf8.parse('4851745953265874');
            var iv = CryptoJS.enc.Utf8.parse('4851745953265874');
            var eUsername = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(vm.username), key,
            {
                keySize: 128 / 8,
                iv: iv,
                mode: CryptoJS.mode.CBC,
                padding: CryptoJS.pad.Pkcs7
            });
            var ePassword = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(vm.password), key,
            {
                keySize: 128 / 8,
                iv: iv,
                mode: CryptoJS.mode.CBC,
                padding: CryptoJS.pad.Pkcs7
            });
            DashboardService.logIn(eUsername.toString(), ePassword.toString()).then(function (response) {               
                window.location.href = "https://accounts.spotify.com/authorize?client_id=92388574f7cd4031bb2f8176a47d45e1&response_type=code&redirect_uri=" + vm.url;
            },
            function (error) {

            });
        }
    }
})();