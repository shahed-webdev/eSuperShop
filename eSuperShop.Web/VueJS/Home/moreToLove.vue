﻿<template>
    <div class="mt-5">
        <h3 class="mb-3">More To Love</h3>

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

                            <!--<div class="p-rating text-right">
                                <span class="sold-count">500 sold</span>
                                <span class="p-discount-percent">10% off</span>
                            </div>-->

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
                baseUrl: "",
                data: [],
                params: { Page: 2, PageSize: 10 },
                isLastPage: false,
                isLoading: false
            }
        },
        methods: {
            getData() {
                axios.get('/home/GetMoreToLove', { params: { Page: 1, PageSize: 10 } }).then(response => {
                    const { IsSuccess, Data } = response.data;
                    if (!IsSuccess) return;

                    this.data = Data.Results;
                });
            },

            loadMore() {
                if (this.isLastPage) return;
                this.isLoading = true;

                axios.get('/home/GetMoreToLove', { params: this.params }).then(response => {
                    const { IsSuccess, Data } = response.data;

                    this.isLastPage = Data.Results.length? false : true;
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