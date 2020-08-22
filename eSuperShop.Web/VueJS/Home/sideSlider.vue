<template>
    <div class="slider-container z-depth-1">
        <div id="carousel-home2" class="carousel slide carousel-fade" data-ride="carousel">
            <!--255*400 px-->
            <div class="carousel-inner" role="listbox">
                <div class="carousel-item" v-for="(item,i) in sideSlider" :class="{ active: i == 0 }" :key="item.SliderId">
                    <img :src="item.ImageUrl" alt="" class="img-fluid">
                </div>
            </div>

            <a class="carousel-control-prev" href="#carousel-home2" role="button" data-slide="prev">
                <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                <span class="sr-only">Previous</span>
            </a>
            <a class="carousel-control-next" href="#carousel-home2" role="button" data-slide="next">
                <span class="carousel-control-next-icon" aria-hidden="true"></span>
                <span class="sr-only">Next</span>
            </a>
        </div>
    </div>
</template>

<script>
    export default {
        data() {
            return {
                sideSlider: []
            }
        },
        mounted() {
            axios.get('/home/GetSliderData', { params: { place: "Side" } }).then(response => {
                this.sideSlider = response.data.Data;
            })
        }
    };
</script>