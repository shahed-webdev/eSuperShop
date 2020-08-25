<template>
    <div>
        <h3>Top Categories This Week</h3>
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
            axios.get('/home/GetCategory', { params: { place: "HomePageTopCatalog", numberOfData: 20} }).then(response => {
                this.data = response.data.Data;
            });
        }
    };
</script>