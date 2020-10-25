<template>
    <div>
        <div class="row">
            <div v-for="(item,i) in data" :key="i" class="col-lg-4 col-sm-6 mb-3">
                <div class="card hoverable card-body">
                    <div class="shop-name">{{item.StoreName}}</div>
                    <div class="shop-description">{{item.StoreTagLine}}</div>
                    <div class="shop-rating mb-2">
                        <i class="fas fa-star"></i>
                        <i class="fas fa-star"></i>
                        <i class="fas fa-star"></i>
                        <i class="fas fa-star"></i>
                        <i class="fas fa-star"></i>
                        <span>{{item.RatingBy}}</span>
                    </div>

                    <!--<div class="photo-box">
                        <div class="box">
                            <img src="~/images/products/9.webp" alt="" />
                        </div>
                        <div class="box">
                            <img src="~/images/products/8.webp" alt="" />
                        </div>
                        <div class="box">
                            <img src="~/images/products/7.webp" alt="" />
                        </div>
                        <div class="box">
                            <img src="~/images/products/6.webp" alt="" />
                        </div>
                    </div>-->
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
        beforeMount() {
            axios.get('/home/GetTopStore', { params: { Page: 1, PageSize: 3} }).then(response => {
                const { IsSuccess, Data } = response.data;
                if (!IsSuccess) return;

                this.data = Data.Results;
            });
        }
    };
</script>