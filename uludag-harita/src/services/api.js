const API_BASE_URL = process.env.NODE_ENV === 'production' 
  ? '/api' 
  : 'http://localhost:5217/api';

class ApiService {
  async makeRequest(url, options = {}) {
    try {
      const response = await fetch(url, {
        headers: {
          'Content-Type': 'application/json',
          'Accept': 'application/json',
          ...options.headers
        },
        ...options
      });

      if (!response.ok) {
        const errorText = await response.text();
        throw new Error(`HTTP ${response.status}: ${errorText}`);
      }

      return await response.json();
    } catch (error) {
      console.error('API Request Error:', error);
      throw error;
    }
  }

  async getRotalar() {
    return this.makeRequest(`${API_BASE_URL}/RotaTanim`);
  }

  async getRota(id) {
    return this.makeRequest(`${API_BASE_URL}/RotaTanim/${id}`);
  }

  async addRota(rotaData) {
    return this.makeRequest(`${API_BASE_URL}/RotaTanim`, {
      method: 'POST',
      body: JSON.stringify(rotaData)
    });
  }
}

export default new ApiService();