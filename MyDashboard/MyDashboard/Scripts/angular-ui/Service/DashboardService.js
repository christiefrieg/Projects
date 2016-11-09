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
    },
    getSpotifyAuth: function (redirectUri) {
        return $.get('https://accounts.spotify.com/authorize?client_id=92388574f7cd4031bb2f8176a47d45e1&response_type=code&redirect_uri=' + redirectUri)
                .then(function (response) {
                    return response;
                });
    },
    getSpotifyToken: function () {
        return $.get('https://accounts.spotify.com/authorize?client_id=92388574f7cd4031bb2f8176a47d45e1&response_type=code&redirect_uri=' + redirectUri)
                .then(function (response) {
                    return response;
                });
    }
};