<template>
    <div v-if="isData" class="mt-4">
        <div class="product-header">
            <h3>Top Stores</h3>
        </div>
        <div class="row">
            <div v-for="(item,i) in data" :key="i" class="col-lg-4 col-sm-6 mb-3">
                <div class="card hoverable card-body">
                    <div class="shop-name">{{item.StoreName}}</div>
                    <div class="shop-description">{{item.StoreTagLine}}</div>
                    <div class="shop-rating mb-2 d-flex align-items-center">
                        <star-rating :max-rating="5"
                                     :rating="item.Rating"
                                     :increment="0.5"
                                     inactive-color="#bbb"
                                     active-color="#F97700"
                                     :star-size="14"
                                     :read-only="true"
                                     :show-rating="false">
                        </star-rating>
                        <span class="ml-2">{{item.RatingBy}}</span>
                    </div>

                    <div class="photo-box">
                        <div v-for="(imgUrl, i) in item.ProductImageUrls" :key="i" class="box">
                            <img :src="imgUrl" alt="" />
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
    import StarRating from 'vue-star-rating';
    export default {
        components: {
            StarRating
        },
        data() {
            return {
                data: [],
                isData: false,
                params: { Page: 2, PageSize: 4 },
                isLastPage: false,
                isLoading: false
            }
        },
        methods: {
            getData() {
                axios.get('/home/GetTopStore', { params: { Page: 1, PageSize: 4 } }).then(response => {
                    const { IsSuccess, Data } = response.data;
                    if (!IsSuccess) return;

                    this.data = Data.Results;
                    this.isData = Data.Results ? true : false;
                });
            },

            loadMore() {
                if (this.isLastPage) return;
                this.isLoading = true;

                axios.get('/home/GetTopStore', { params: this.params }).then(response => {
                    const { IsSuccess, Data } = response.data;

                    this.isLastPage = Data.Results.length ? false : true;
                    this.isLoading = false;

                    if (!IsSuccess) return;

                    this.data.push(...Data.Results);
                    this.params.Page++;
                });
            }
        },
        beforeMount() {
            this.getData();
        }
    }
</script>