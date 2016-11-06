angular.module('Dashboard.controllers.main', ['ui.bootstrap']).
controller('MainController', function ($scope, $compile) {
    var vm = this;
    vm.showWarning = false;
    vm.warningMessage = '';

    $scope.activateView = function (ele) {
        $compile(ele.contents())($scope);
        $scope.$apply();
    };

    $scope.displayWarningAlert = function (message) {
        vm.showWarning = true;
        vm.warningMessage = message;
    }

    $scope.warningAlertClosed = function () {
        vm.showWarning = false;
    }
});