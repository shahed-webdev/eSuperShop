﻿<template>
    <div v-if="isData" class="mt-5">
        <div class="product-header">
            <h3>Top Rated</h3>
            <a class="black-text" href="/Product/TopRated">View More<i class="far fa-eye ml-1"></i></a>
        </div>
    
        <div v-for="i in Math.ceil(data.length / 5)" class="row fivecolumns">
            <div v-for="(item,i) in data.slice((i - 1) * 5, i * 5)" :key="i" class="col-lg-2 col-md-4 mb-4">
                <div class="card hoverable h-100">
                    <div class="view overlay">
                        <img class="card-img-top" :src="baseUrl+'/thumb_'+item.ImageFileName" :alt="item.Name">
                        <a :href="'/item/'+item.SlugUrl"><div class="mask rgba-white-slight"></div></a>
                    </div>
                    <div class="card-body">
                        <p class="product-name">{{item.Name}}</p>
                        <div class="pricing-area">
                            <div class="p-price">
                                <span>৳{{item.Price}}</span>
                                <span v-if="item.OldPrice" class="p-discount d-block">৳{{item.OldPrice}}</span>
                            </div>
                            <div class="p-rating text-right">
                                <div class="d-flex align-items-center">
                                    <div class="mr-2">
                                        <star-rating :max-rating="5"
                                                     :rating="item.Rating"
                                                     :increment="0.5"
                                                     inactive-color="#bbb"
                                                     active-color="#F97700"
                                                     :star-size="14"
                                                     :read-only="true"
                                                     :show-rating="false">
                                        </star-rating>
                                    </div>
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
    import StarRating from 'vue-star-rating'
    export default {
        components: {
            StarRating
        },
        data() {
            return {
                baseUrl:"",
                data: [],
                isData: false
            }
        },
        beforeMount() {
            //base url
            axios.get('/home/GetBaseUrl').then(response => {
                this.baseUrl = response.data;
            });

            axios.get('/home/GetTopRated', { params: { Page: 1, PageSize: 5 } }).then(response => {
                const { IsSuccess, Data } = response.data;
                if (!IsSuccess) return;

                this.data = Data.Results;
                this.isData = Data.Results ? true : false;
            });
        }
    }
</script>