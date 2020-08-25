<template>
    <div class="category-container z-depth-1">
        <div class="category-title">
            <i class="fas fa-list"></i>
            <span>Categories</span>
        </div>
        <ul>
            <li v-for="(item,i) in data" :key="i">
                <a :href="'/Category/Products/'+item.SlugUrl">
                    <i class="fas fa-caret-right"></i>
                    {{item.CatalogName}}
                </a>
            </li>
        </ul>
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
            axios.get('/home/GetCategory', { params: { place: "HomePageMain", numberOfData:12 } }).then(response => {
                this.data = response.data.Data;
            });
        }
    };
</script>