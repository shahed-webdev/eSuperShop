<template>
    <div class="quick-delivery card card-body py-4" :class="{loading: !data.length}">
        <div v-if="data.length" class="offer-header">
            <h4>Delivery in 24 hours</h4>
            <span>dhaka, narayanganj, chittagong, sylhet, and gazipur city</span>
        </div>

        <div class="row">
            <div v-for="(item,i) in data" :key="i" class="col-lg-2 col-sm-6 col-6 mb-4">
                <div class="card h-100">
                    <div class="view overlay h-100">
                        <img class="card-img-top" :src="baseUrl+'/thumb_'+item.ImageFileName" alt="">
                        <a :href="'/category/'+item.SlugUrl">
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
                baseUrl: "",
                data: []
            }
        },
        beforeMount() {
            //base url
            axios.get('/home/GetBaseUrl').then(response => {
                this.baseUrl = response.data;
            });

            axios.get('/home/GetCategory', { params: { place: "HomePageQuickDelivery", numberOfData: 6 } }).then(response => {
                const { IsSuccess, Data } = response.data;
                if (!IsSuccess) return;

                this.data = Data;
            });
        }
    };
</script>