<template>
    <div v-if="isData" class="mt-5">
        <div class="product-header">
            <h3>Top Rated</h3>
            <a href="/Product/TopRated">View More<i class="far fa-eye ml-1"></i></a>
        </div>
    
        <div class="row">
            <div v-for="(item,i) in data" :key="i" class="col-lg-3 col-sm-6">
                <div class="card hoverable h-100">
                    <div class="view overlay">
                        <img class="card-img-top" :src="item.ImageUrl" :alt="item.Name">
                        <a>
                            <div class="mask rgba-white-slight"></div>
                        </a>
                    </div>
                    <div class="card-body pb-1">
                        <p class="product-name">{{item.Name}}</p>
                        <div class="pricing-area">
                            <div class="p-price">
                                <span>৳{{item.Price}}</span>
                                <span v-if="item.OldPrice" class="p-discount d-block">৳{{item.OldPrice}}</span>
                            </div>
                            <div class="p-rating text-right">
                                <div>
                                    <i :class="['fas', 'fa-star', { 'text-muted': !item.Rating}]"></i>
                                    <span :class="['rating-count', { 'text-muted': !item.RatingBy}]">{{item.RatingBy}}</span>
                                </div>
                                <span v-if="item.OldPrice" class="p-discount-percent">{{(100-((100*item.Price)/item.OldPrice)).toFixed(2)}}% off</span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    export default {
        data() {
            return {
                data: [],
                isData: false
            }
        },
        beforeMount() {
            axios.get('/home/GetTopRated', { params: { Page: 1, PageSize: 4 } }).then(response => {
                const { IsSuccess, Data } = response.data;
                if (!IsSuccess) return;

                this.data = Data.Results;
                this.isData = Data.Results ? true : false;
            });
        }
    }
</script>