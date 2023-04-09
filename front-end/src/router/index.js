import { createRouter, createWebHistory } from 'vue-router';
import Home from '../views/HomePage.vue';
import Article from '../views/ArticleView.vue';
import Admin from '../views/AdminPanel.vue';
import Login from '../views/LoginPanel.vue';

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/article/:id',
    name: 'Article',
    component: Article
  },
  {
    path: '/admin',
    name: 'Admin',
    component: Admin,
    beforeEnter: (to, from, next) => {
      const token = localStorage.getItem('token');
      if (!token) {
        next('/login');
      } else {
        next();
      }
    }
  },
  {
    path: '/login',
    name: 'Login',
    component: Login
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes
});

export default router;
