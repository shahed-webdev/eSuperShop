<template>
    <div class="mt-5">
        <div class="product-header">
            <h3>Flash Deals</h3>
            <a>View More<i class="far fa-eye ml-1"></i></a>
        </div>

        <div class="row">
            <div v-for="(item,i) in data" :key="i" class="col-lg-3 col-sm-6">
                <div class="card hoverable mb-3">
                    <div class="view overlay">
                        <img class="card-img-top" :src="item.ImageUrl" :alt="item.Name">
                        <a>
                            <div class="mask rgba-white-slight"></div>
                        </a>
                    </div>
                    <div class="card-body">
                        <p class="product-name">{{item.Name}}</p>
                        <div class="pricing-area">
                            <div class="p-price">
                                <span>৳{{item.OldPrice}}</span>
                                <!--<span class="p-discount d-block">৳500</span>-->
                            </div>
                            <div class="p-rating text-right">
                                <div>
                                    <i class="fas fa-star"></i>
                                    <i class="fas fa-star"></i>
                                    <i class="fas fa-star"></i>
                                    <i class="fas fa-star"></i>
                                    <i class="fas fa-star"></i>
                                    <span class="rating-count">{{item.Rating}}</span>
                                </div>
                                <!--<span class="p-discount-percent">10% off</span>-->
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
               data:[]
            }
        },
        mounted() {
            axios.get('/home/GetFlashDeals', { params: { Page: 1, PageSize: 4} }).then(response => {
                this.data = response.data.Data;
            });
        }
    };
</script>