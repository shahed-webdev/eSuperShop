<template>
    <div class="card card-body py-4">
        <div class="offer-header">
            <h4>Delivery in 24 hours</h4>
            <span>dhaka, narayanganj, chittagong, sylhet, and gazipur city</span>
        </div>

        <div class="row">
            <div v-for="(item,i) in data" :key="i" class="col-lg-2 col-sm-6 col-6">
                <div class="box">
                    <img :src="item.ImageUrl" alt="" />
                    <div class="box-title">{{item.CatalogName}}</div>
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