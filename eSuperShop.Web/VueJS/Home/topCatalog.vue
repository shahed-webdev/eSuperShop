<template>
    <div>
        <h3>Top Categories This Week</h3>

        <div class="row">
             <div v-for="(item,i) in data" :key="i" class="col-lg-2 col-sm-6 col-6">
                 <div class="box">
                     <img :src="item.ImageUrl" alt="" />
                     <div :title="item.CatalogName" class="box-title">{{item.CatalogName}}</div>
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