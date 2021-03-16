<template>
    <div>
        <div v-for="(category, i) in data" :key="i" class="mb-4">
            <h4 class="category-title mb-4">{{category.Name}}</h4>

            <div class="row">
                <div v-for="product in category.Products" :key="product.ProductId" class="col-lg-4 col-6 mb-4">
                    <div class="card hoverable h-100">
                        <div class="view overlay">
                            <img class="card-img-top" :src="baseUrl+'/thumb_'+product.ImageFileName" :alt="product.Name">
                            <a :href="'/item/'+product.SlugUrl">
                                <div class="mask rgba-white-slight"></div>
                            </a>
                        </div>

                        <div class="product-name">
                            <p class="mb-0">{{product.Name}}</p>
                            <strong>৳{{product.Price}}</strong>
                            <span v-if="product.OldPrice" class="p-discount">৳{{product.OldPrice}}</span>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="loader-style" v-if="isLoading">
            <i class="fas fa-circle-notch fa-spin fa-3x"></i>
        </div>
        <div v-if="!isLastPage" class="d-flex justify-content-center mt-4">
            <button @click="loadMore" :disabled="isLoading" class="btn btn-danger">{{isLoading? "loading..":" Load More"}}</button>
        </div>
    </div>
</template>

<script>
    export default {
        props: ['vendorId'],
        data() {
            return {
                data: [],
                params: { Page: 2, PageSize: 2, VendorId: +this.vendorId },
                isLastPage: false,
                isLoading: false
            }
        },
        methods: {
            getData() {
                axios.get('/Store/GetCategoryProduct', { params: { Page: 1, PageSize: 2, VendorId: +this.vendorId } }).then(response => {
                    const { IsSuccess, Data } = response.data;
              
                    if (!IsSuccess) return;

                    this.data = Data.Results;
                });
            },

            loadMore() {
                if (this.isLastPage) return;
                this.isLoading = true;

                axios.get('/Store/GetCategoryProduct', { params: this.params }).then(response => {
                    const { IsSuccess, Data } = response.data;
                
                    this.isLastPage = Data.Results.length ? false : true;
                    this.isLoading = false;

                    if (!IsSuccess) return;

                    this.data.push(...Data.Results);
                    this.params.Page++;
                }).catch(err => {
                    console.log(err);
                    this.isLoading = false;
                });
            },
        },
        beforeMount() {
            this.getData();

            //base url
            axios.get('/home/GetBaseUrl').then(response => {
                this.baseUrl = response.data;
            });
        }
    }
</script>