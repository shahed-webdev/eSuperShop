<template>
    <div class="card card-body py-4">
        <div class="offer-header">
            <h4>Delivery in 24 hours</h4>
            <span>dhaka, narayanganj, chittagong, sylhet, and gazipur city</span>
        </div>

        <div class="row">
            <div v-for="(item,i) in data" :key="i" class="col-lg-3 col-sm-6 col-6 mb-4">
                <div class="card h-100">
                    <div class="view overlay h-100">
                        <img class="card-img-top" :src="item.ImageUrl" alt="">
                        <a href="#!">
                            <div class="mask rgba-white-slight"></div>
                        </a>
                    </div>
                    <div class="card-title text-center">{{item.CatalogName}}</div>
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
            axios.get('/home/GetCategory', { params: { place: "HomePageQuickDelivery", numberOfData:6} }).then(response => {
                this.data = response.data.Data;
            });
        }
    };
</script>