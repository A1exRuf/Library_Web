import axios from 'axios';

const apiClient = axios.create({
  baseURL: 'https://localhost:7196/api',
  headers: {
    'Content-Type': 'application/json',
  },
});

const token = localStorage.getItem("accessToken");
if (token) {
  apiClient.defaults.headers.common["Authorization"] = `Bearer ${token}`;
}

export default apiClient;
