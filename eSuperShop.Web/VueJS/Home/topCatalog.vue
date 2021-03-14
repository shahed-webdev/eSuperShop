<template>
    <div>
        <h3>Top Categories This Week</h3>
        <div v-for="i in Math.ceil(data.length / 5)" class="row fivecolumns">
            <div v-for="(item,i) in data.slice((i - 1) * 5, i * 5)" :key="i" class="col-lg-2 col-md-4 mb-4">
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
                baseUrl:"",
                data: []
            }
        },
        beforeMount() {
            //base url
            axios.get('/home/GetBaseUrl').then(response => {
                this.baseUrl = response.data;
            });

            axios.get('/home/GetCategory', { params: { place: "HomePageTopCatalog", numberOfData: 10 } }).then(response => {
                const { IsSuccess, Data } = response.data;
                if (!IsSuccess) return;

                this.data = Data;
            });
        }
    };
</script>