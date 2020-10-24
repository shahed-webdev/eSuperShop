<template>
    <div id="main-slider" class="slider-container z-depth-1">
        <div id="carousel-home" class="carousel slide carousel-fade" data-ride="carousel">
            <!--540*400 px-->
            <div class="carousel-inner" role="listbox">
                <div class="carousel-item" v-for="(item,i) in mainSlider" :class="{ active: i == 0 }" :key="item.SliderId">
                    <img :src="item.ImageUrl" alt="" class="img-fluid">
                </div>
            </div>

            <a class="carousel-control-prev" href="#carousel-home" role="button" data-slide="prev">
                <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                <span class="sr-only">Previous</span>
            </a>
            <a class="carousel-control-next" href="#carousel-home" role="button" data-slide="next">
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
                mainSlider: []
            }
        },
        beforeMount() {
            axios.get('/home/GetSliderData', { params: { place: "Main" } }).then(response => {
                const { IsSuccess, Data } = response.data;
                if (!IsSuccess) return;

                this.mainSlider = Data;
            });
        }
    };
</script>