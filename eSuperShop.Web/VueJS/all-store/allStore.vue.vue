<template>
    <div class="mt-4">
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
                isLastPage: true
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

            getDataOnDemand(params) {
                window.onscroll = () => {
                    if (!this.isLastPage) return;

                    const element = document.documentElement;
                    const bottomOfWindow = element.scrollTop + window.innerHeight === element.offsetHeight;
    
                    if (bottomOfWindow) {
                        axios.get('/home/GetTopStore', { params }).then(response => {
                            const { IsSuccess, Data } = response.data;
             
                            this.isLastPage = IsSuccess;

                            if (!IsSuccess) return;

                            this.data.push(...Data.Results);
                            this.params.Page++;
                        });
                    }
                };
            },
        },
        beforeMount() {
            this.getData();
        },
        mounted() {
            this.getDataOnDemand(this.params);
        }
    }
</script>