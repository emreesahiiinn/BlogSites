<template>
  <v-container>
    <v-tabs v-model="tab" background-color="transparent">
      <v-tab>Log In</v-tab>
      <v-tab>Register</v-tab>
    </v-tabs>
    <v-card>
      <v-card-text>
        <v-form v-if="tab === 0" ref="loginForm" @submit.prevent="login">
          <v-text-field v-model="Email" label="Email"></v-text-field>
          <v-text-field v-model="Password" label="Password" type="password"></v-text-field>
          <v-btn type="submit" color="primary">Log In</v-btn>
        </v-form>
        <v-form v-if="tab === 1" ref="registerForm" @submit.prevent="register">
          <v-text-field v-model="FirstName" label="Name"></v-text-field>
          <v-text-field v-model="LastName" label="LastName"></v-text-field>
          <v-text-field v-model="Email" label="Email"></v-text-field>
          <v-text-field v-model="Password " label="Password" type="password"></v-text-field>
          <v-btn type="submit" color="primary">Register</v-btn>
        </v-form>
      </v-card-text>
    </v-card>
  </v-container>
</template>

<script>
  import axios from 'axios';
  export default {
    name: 'LoginForm',
    data() {
      return {
        tab:'',
        FirstName: '',
        LastName:'',
        Email: '',
        Password: '',
      };
    },
    methods: {
      async login() {
        try {
          const response = await axios.post('https://localhost:4000/api/authors/login', {
            email: this.Email,
            password: this.Password,
          });
          localStorage.setItem('token', response.data.token);
          this.$router.push('/admin');
        } catch (error) {
          console.error(error);
          alert("Giriş yapılamadı. Lütfen bilgilerinizi kontrol edin.");
        }
      },
      async register() {
        try {
          await axios.post('http://localhost:4000/api/authors/register', {
            name: this.FirstName,
             lastname:this.LastName,
            email: this.email,
            password: this.password,
          });
          alert("Kayıt işlemi başarılı. Şimdi giriş yapabilirsiniz.");
          this.$refs.loginForm.reset();
          this.tab = 0;
        } catch (error) {
          console.error(error);
        }
      },
    },
  };
</script>
