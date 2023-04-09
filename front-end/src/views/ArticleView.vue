<template>
  <div class="article">
    <div v-if="!loading">
      <h1>{{ article.title }}</h1>
      <p>{{ article.content }}</p>
      <div class="comment-list">
        <h2>Yorumlar:</h2>
        <v-list>
          <v-list-item v-for="comment in comments" :key="comment.id">
            <v-list-item-content>
              <v-list-item-title>
                {{ comment.name }} {{ comment.lastname }}
              </v-list-item-title>
              <v-list-item-subtitle>
                {{ comment.icerik }}
              </v-list-item-subtitle>
            </v-list-item-content>
          </v-list-item>
        </v-list>
      </div>
      <div class="comments">
        <h2>Yorum yap..</h2>
        <v-form @submit.prevent="submitComment">
          <v-container>
            <v-row>
              <v-col cols="12" md="6">
                <v-text-field v-model="comment.name" label="Ad" required></v-text-field>
              </v-col>
              <v-col cols="12" md="6">
                <v-text-field v-model="comment.lastname" label="Soyad" required></v-text-field>
              </v-col>
            </v-row>
            <v-text-field v-model="comment.email" label="Email" type="email" required></v-text-field>
            <v-textarea v-model="comment.icerik" label="Yorum" required></v-textarea>
            <v-btn color="primary" type="submit">Gönder</v-btn>
          </v-container>
        </v-form>
      </div>
    </div>
    <div v-else>
      <p>Yükleniyor...</p>
    </div>
  </div>
</template>
<script>
import axios from "axios";
export default {
  name: "ArticleView",
  data() {
    return {
      article: {},
      loading: true,
      comment: {
        name: "",
        lastname: "",
        email: "",
        icerik: "",
      },
      comments: [],
    };
  },
  mounted() {
    const articleId = this.$route.params.id;
    axios
      .get(`https://localhost:4000/api/Article/${articleId}`)
      .then((response) => {
        this.article = response.data;
        this.loading = false;
      })
      .catch((error) => {
        console.log(error);
      });
    this.getComments();
  },
  methods: {
    submitComment() {
      this.comment.articleId = this.article.id;
      axios
        .post(`https://localhost:4000/api/comments`, this.comment)
        .then((response) => {
          console.log(response.data);
          this.comment.name = "";
          this.comment.lastname = "";
          this.comment.email = "";
          this.comment.icerik = "";
          this.getComments();
        })
        .catch((error) => {
          console.log(error);
        });
    },

    getComments() {
      const articleId = this.article.id;
      axios
        .get(`https://localhost:4000/api/comments?articleId=${articleId}`)
        .then((response) => {
          this.comments = response.data.filter(comment => comment.articleId === articleId);
        })
        .catch((error) => {
          console.log(error);
        });
    },

  },
};
</script>

<style scoped>
.article {
  max-width: 800px;
  margin: 0 auto;
  padding: 20px;
}

.article img {
  max-width: 100%;
  margin-top: 20px;
}

.comments {
  margin-top: 30px;
}
</style>