var DashboardService = {
    logIn: function (username, password) {
        return $.post('/Account/LogInUser?username=' + username + "&password=" + password)
                .then(function (response) {
                    return response;
                });
    },
    getCurrentUser: function () {
        return $.get('/Account/GetCurrentUser')
                .then(function (response) {
                    return response;
                });
    }
};