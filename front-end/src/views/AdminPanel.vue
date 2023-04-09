<template>
    <v-card>
        <h2>Admin Paneli</h2>
        <v-form @submit.prevent="addArticle">
            <v-text-field v-model="Item.Title" label="Başlık" required></v-text-field>
            <v-select v-model="Category" :items="categories" label="Kategori"></v-select>
            <v-textarea v-model="Item.Content" label="İçerik" required></v-textarea>
            <v-btn type="submit" color="primary" @click="addArticle(Item)">Makale Ekle</v-btn>
        </v-form>
        <v-simple-table>
            <template v-slot:default>
                <thead>
                    <tr>
                        <th>Başlık</th>
                        <th>Category ID</th>
                        <th>İşlemler</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="article in articles" :key="article.ArticleId">
                        <td>{{ article.title }}</td>
                        <td>{{ article.categories }}</td>
                        <td>
                            <v-btn color="primary" @click="editItem(article)">Güncelle</v-btn>
                            <v-btn color="error" @click="deleteItem(article)">Sil</v-btn>
                        </td>
                    </tr>
                </tbody>
            </template>
        </v-simple-table>
        <v-dialog v-model="deleteDialog" max-width="500px">
            <v-card>
                <v-card-title class="text-h5">Silmek istediğinize emin misiniz?</v-card-title>
                <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn color="blue darken-1" text @click="closeDelete">İptal</v-btn>

                    <v-btn color="blue darken-1" text @click="deleteItemConfirm">Sil</v-btn>
                    <v-spacer></v-spacer>
                </v-card-actions>
            </v-card>
        </v-dialog>
        <v-dialog v-model="dialog" max-width="500">
            <v-card>
                <v-card-title>
                    <span class="headline">{{ dialogTitle }}</span>
                </v-card-title>
                <v-card-text>
                    <v-text-field v-model="editedItem.Title" label="Başlık"></v-text-field>
                    <v-select v-model="myCategory" :items="categories" label="Kategori"></v-select>
                    <v-textarea v-model="editedItem.Content" label="İçerik"></v-textarea>
                </v-card-text>
                <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn color="blue darken-1" text @click="dialog = false">İptal</v-btn>
                    <v-btn color="blue darken-1" text @click="saveArticle">Kaydet</v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
    </v-card>
    <v-btn  color="red" dark @click="$router.push('/login')">EXIT</v-btn>
</template>
<script>
import axios from 'axios';
export default {
    data() {
        return {
            Category: '',
            dialog: false,
            deleteDialog: false,
            dialogTitle: '',
            editedIndex: -1,
            categories: [],
            myCategory:"",
            Item:{
                Title: "",
                CategoryId: "",
                Content: "", 
            },
            editedItem: {
                Title: "",
                CategoryId: "",
                Content: "",
            },
            defaultItem: {
                ArticleId: "",
                Title: "",
                Content: "",
                CategoryId: "",
                CommentId: "",
                AuthorId: "",
            },
            articles: [],
        }
    },
    created() {
        this.getArticles();
        this.getCategories();
    },
    methods: {
        getArticles() {
            axios
                .get("https://localhost:4000/api/Article")
                .then(response => {
                    this.articles = response.data;
                })
                .catch(error => {
                    console.log(error);
                });
        },
        close() {
            this.dialog = false;
        },
        getCategories() {
            axios
                .get("https://localhost:4000/api/category")
                .then(response => {
                    for(var i = 0; i<response.data.length; i++){
                        this.categories.push (response.data[i].name)
                    }
                })
                .catch(error => {
                    console.log(error);
                });
        },
        addArticle() {
            const categoryId = this.categories.find((category) => category.name === this.Category)?.id || '';
            const newItem = {
                ...this.editedItem,
                CategoryId: categoryId,
            };

            axios
                .post('https://localhost:4000/api/Article/', newItem)
                .then((response) => {
                    console.log(response);
                    this.Item = {};
                    this.getArticles();
                })
                .catch((error) => {
                    console.log(error);
                });
        },
        editItem(item) {
            this.editedItem = Object.assign({}, item);
             this.myCategory = this.categories.filter(category => category===this.categories)
            console.log(item)
            this.dialog = true;
        },
        cancelEdit() {
            this.dialog = false;
        },
        saveArticle() {
            if (this.editedItem.id) {
                axios
                    .put(`https://localhost:4000/api/Article/${this.editedItem.id}`, this.editedItem)
                    .then(response => {
                        const index = this.articles.findIndex(article => article.id === this.editedItem.id);
                        if (index > -1) {
                            this.articles[index] = response.data;
                        }
                        this.dialog = false;
                    })
                    .catch(error => {
                        console.log(error);
                    });
            } else {
                axios
                    .post("https://localhost:4000/api/Article/", this.editedItem)
                    .then(response => {
                        this.articles.push(response.data);
                        this.dialog = false;
                    })
                    .catch(error => {
                        console.log(error);
                    });
            }
        },
        deleteItem(item) {
            this.editedItem = Object.assign({}, item);
            this.deleteDialog = true;
        },
        closeDelete() {
            this.editedItem = {};
            this.deleteDialog = false;
        },
        deleteItemConfirm() {
            axios.delete(`https://localhost:4000/api/Article/${this.editedItem.id}`)
                .then(response => {
                    console.log(response);
                    const index = this.articles.findIndex(article => article.id === this.editedItem.id);
                    if (index > -1) {
                        this.articles.splice(index, 1);
                    }
                    this.editedItem = {};
                    this.deleteDialog = false;
                })
                .catch(error => {
                    console.log(error);
                });
        }
    }
}
</script>
