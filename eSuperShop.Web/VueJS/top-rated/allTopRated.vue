<template>
    <div v-if="isData" class="mt-5">
        <div class="product-header">
            <h3>Top Rated</h3>
        </div>
    
        <div class="row">
            <div v-for="(item,i) in data" :key="i" class="col-lg-3 col-sm-6 mb-4">
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
                isData: false,
                params: { Page: 2, PageSize: 4 },
                isLastPage: true
            }
        },
        methods: {
            getData() {
                axios.get('/home/GetTopRated', { params: { Page: 1, PageSize: 4 } }).then(response => {
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
                        axios.get('/home/GetTopRated', { params }).then(response => {
                            const { IsSuccess, Data } = response.data;
                            console.log(response)
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