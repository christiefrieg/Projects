(function () {
    "use strict";

    angular
        .module("Dashboard.controllers.LoginController",[])
        .controller("LoginController", LoginController);

    function LoginController($scope) {
        var vm = this;
        vm.username = null;
        vm.password = null;
        vm.url = null;
        vm.init = function (url){
            if(url != null)
                vm.url = url;
        }
        vm.logIn = function(){
            var eUsername = CryptoJS.AES.encrypt(vm.username, "4851745953265874");
            var ePassword = CryptoJS.AES.encrypt(vm.password, "4851745953265874");
            //var decrypted = CryptoJS.AES.decrypt(encrypted, "Secret Passphrase");
            DashboardService.logIn(eUsername, ePassword).then(function (response) {
                if (vm.url != null)
                    window.open(vm.url);
                else
                    window.open('Home/Index');
            },
            function (error) {

            });
        }
    }
})();