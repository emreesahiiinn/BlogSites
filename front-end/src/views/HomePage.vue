<template>
  <v-container fluid>
    <v-row>
      <v-col cols="12">
        <v-card>
          <v-card-text>
            <h1 class="text-center">Cup Of jo</h1>
            <v-row>
              <v-col cols="12" class="d-flex justify-space-between align-center">
                <v-chip-group v-model="categoryFilter">
                  <v-chip class="category" @click="filterArticles('all')">All</v-chip>
                  <v-chip class="category" v-for="category in categories" :key="category.id" @click="filterArticles(category.name)">{{ category.name }}</v-chip>
                </v-chip-group>
                <v-btn color="primary" @click="$router.push('/login')">Login</v-btn>
              </v-col>
            </v-row>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="12" md="4" lg="3" v-for="article in filteredArticles" :key="article.id">
        <v-card class="article" @click="viewArticle(article.id)">
          <v-img :src="article.imageUrl" height="250"></v-img>
          <v-card-title>{{ article.title }}</v-card-title>
          <v-card-text>{{ article.content }}</v-card-text>  
        </v-card>
      </v-col>
      <v-col v-if="filteredArticles.length === 0" cols="12">
        <v-card>
          <v-card-text>
            No articles found for the selected category.
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import axios from 'axios';
export default {
  name: 'HomePage',
  data() {
    return {
      articles: [],
      categories: [],
      categoryFilter: 'all'
    };
  },
  mounted() {
    this.fetchArticle();
    this.fetchCategories();
  },
  computed: {
    filteredArticles() {
      if (this.categoryFilter === 0) {
        return this.articles;
      } else {
        const filtered = this.articles.filter(article => article.categoryId === this.categoryFilter);
        return filtered;
      }
    }
  },
  methods: {
    fetchArticle() {
      axios.get('https://localhost:4000/api/Article')
        .then(response => {
          this.articles = response.data;
        })
        .catch(error => {
          console.log(error);
        });
    },
    fetchCategories() {
      axios.get('https://localhost:4000/api/category')
        .then(response => {
          this.categories = response.data;
        })
        .catch(error => {
          console.log(error);
        });
    },
    filterArticles(categoryName) {
      if (categoryName === 'all') {
        this.categoryFilter = 'all';
      } else {
        const category = this.categories.find(category => category.name === categoryName);
        if (category) {
          this.categoryFilter = category.id;
        } else {
          console.log(`Could not find category with name "${categoryName}"`);
        }
      }
    },
    viewArticle(id) {
      this.$router.push(`/article/${id}`);
    }
  }
};
</script>
<style scoped>
body {
  font-family: 'Times New Roman', Times, serif;
}

.text-center {
  font-size: 30px;
  text-align: center;
}

.category {
  font-size: 16px;
  font-weight: bold;
  text-transform: uppercase;
  letter-spacing: 1px;
  cursor: pointer;
  transition: color 0.2s ease-in-out;
  display: flex;
  justify-content: space-between;
  margin-bottom: 20px;
}

.article {
  margin-bottom: 20px;
  cursor: pointer;
}

.article .title {
  font-size: 20px;
}</style>