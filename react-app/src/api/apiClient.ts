import axios from 'axios';
import Cookies from "js-cookie";

const apiClient = axios.create({
  baseURL: 'https://localhost:5001/api',
  headers: {
    'Content-Type': 'application/json',
  },
  withCredentials: true
});

apiClient.interceptors.request.use(
  config => {
    const token = Cookies.get('accessToken');
    if (token) {
      config.headers['Authorization'] = `Bearer ${token}`;
    }
    return config;
  },
  error => Promise.reject(error)
);

apiClient.interceptors.response.use(
  response => response,
  async error => {
    const originalRequest = error.config;
    if (error.response?.status === 401 && !originalRequest._retry) {
      originalRequest._retry = true;
      try {
        const refreshToken = Cookies.get('refreshToken');
        const response = await apiClient.post('user/loginwithrefreshtoken', {
          refreshToken,
        });
        const { accessToken, refreshToken: newRefreshToken } = response.data;
        Cookies.set('accessToken', accessToken, { expires: 7 });
        Cookies.set('refreshToken', newRefreshToken, { expires: 7 });

        originalRequest.headers['Authorization'] = `Bearer ${accessToken}`;

        return apiClient(originalRequest);
      } catch (refreshError) {
        Cookies.remove('accessToken');
        Cookies.remove('refreshToken');
        return Promise.reject(refreshError);
      }
    }
    return Promise.reject(error);
  }
);

export default apiClient;
