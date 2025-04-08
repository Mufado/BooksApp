import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
import axios from 'axios'

axios.defaults.baseURL = 'https://localhost:7161'

createApp(App).mount('#app')
