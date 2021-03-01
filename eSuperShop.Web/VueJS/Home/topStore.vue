<template>
    <div>
        <div class="product-header">
            <h3>Top Stores</h3>
            <a href="/Product/AllStores">View More<i class="far fa-eye ml-1"></i></a>
        </div>
        <div class="row">
            <div v-for="(item,i) in data" :key="i" class="col-lg-3 col-sm-6 mb-3">
                <a :href="/item.StoreSlugUrl">
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
                </a>
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
                data: []
            }
        },
        beforeMount() {
            axios.get('/home/GetTopStore', { params: { Page: 1, PageSize: 4 } }).then(response => {
                const { IsSuccess, Data } = response.data;
                if (!IsSuccess) return;

                this.data = Data.Results;
            });
        }
    };
</script>