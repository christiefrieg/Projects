(function () {
    'use strict';
    angular.module("Dashboard.controllers.HomeController", [])
                .controller("HomeController", function ($scope) {
                    var vm = this;
                    vm.username = null;
                    vm.password = null;
                    vm.url = null;
                    vm.init = function (uri) {
                        let service = new DashboardService();
                        service.getSpotifyAuth(uri)
                            .then(function (response) {

                            });
                    }
                })
})();