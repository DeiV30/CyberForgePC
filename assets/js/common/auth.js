const API_URL = 'http://localhost:56974/api';

function refreshAccessToken() {
    const access = localStorage.getItem('access');
    const refreshToken = localStorage.getItem('refresh');

    return axios.post(`${API_URL}/authentication/refresh`, {
        accessToken: access,
        refreshToken: refreshToken
    })
        .then(response => {
            const newAccessToken = response.data.data.newAccessToken;
            const newRefreshToken = response.data.data.newRefreshToken;

            localStorage.setItem('access', newAccessToken);
            localStorage.setItem('refresh', newRefreshToken);

            return newAccessToken;
        })
        .catch(error => {
            Swal.fire({
                icon: 'error',
                title: 'Fallo del sistema',
                text: 'Error al actualizar el token',
                confirmButtonText: 'OK'
            }).then((result) => {
                if (result.isConfirmed) {
                    window.location.href = '/index.html';
                }
            });
            return Promise.reject(error);
        });
}

function makeAuthenticatedRequest(config) {
    const accessToken = localStorage.getItem('access');

    if (!accessToken) {
        window.location.href = '/index.html';
        return Promise.reject('Usuario no autenticado');
    }

    return axios({
        ...config,
        headers: {
            ...config.headers,
            'Authorization': `Bearer ${accessToken}`
        }
    }).catch(error => {
        if (error.response && error.response.status === 401) {
            return refreshAccessToken().then(newAccessToken => {
                return axios({
                    ...config,
                    headers: {
                        ...config.headers,
                        'Authorization': `Bearer ${newAccessToken}`
                    }
                });
            });
        }
        return Promise.reject(error);
    });
}

function makeAnonymousRequest(config) {
    return axios(config);
}

function handleRequestError(error) {
    if (error.response) {
        if (error.response.status === 403) {
            Swal.fire({
                icon: 'error',
                title: 'Lo sentimos',
                text: error.response.data.message,
            });
        } else {
            Swal.fire({
                icon: 'error',
                title: 'Solicitud fallida',
                text: error.response.data.message,
            });
        }
    } else {
        Swal.fire({
            icon: 'error',
            title: 'Solicitud fallida',
            text: error.response.data.message,
        });
    }
}