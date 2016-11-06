var DashboardService = {
    logIn: function (username, password) {
        return $.get('/Controllers/Account/LogInUser?username=' + username + "&password=" + password)
                .then(function (response) {
                    return response;
                });
    },
    getCurrentUser: function () {
        return $.get('/Controllers/Account/GetCurrentUser')
                .then(function (response) {
                    return response;
                });
    }
};