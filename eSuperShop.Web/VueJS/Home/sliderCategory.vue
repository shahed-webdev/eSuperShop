<template>
    <div class="row">
        <div class="col-lg-3 mb-3">
            <div :class="{loading: !catalog.length}" class="category-container z-depth-1">
                <div v-if="catalog.length" class="category-title">
                    <i class="fas fa-list"></i>
                    <span>Categories</span>
                </div>
                <ul>
                    <li v-for="(item,i) in catalog" :key="i">
                        <a :href="'/category/'+item.SlugUrl">
                            <i class="fas fa-caret-right"></i>
                            {{item.CatalogName}}
                        </a>
                    </li>
                </ul>
            </div>
        </div>

        <div class="col-lg-6 mb-3 col-8">
            <div id="main-slider" class="slider-container z-depth-1" :class="{loading: !mainSlider.length}">
                <div id="carousel-home" class="carousel slide carousel-fade" data-ride="carousel">
                    <ol class="carousel-indicators">
                        <li v-for="(item,i) in mainSlider" data-target="#carousel-home" :data-slide-to="i" :class="{ active: i == 0 }" :key="item.SliderId"></li>
                    </ol>

                    <!--540*400 px-->
                    <div class="carousel-inner" role="listbox">
                        <div class="carousel-item" v-for="(item,i) in mainSlider" :class="{ active: i == 0 }" :key="item.SliderId">
                            <img :src="baseUrl+'/'+item.ImageFileName" alt="" class="img-fluid">
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
        </div>

        <div class="col-lg-3 col-4">
            <div class="slider-container z-depth-1" :class="{loading: !sideSlider.length}">
                <div id="carousel-home2" class="carousel slide carousel-fade" data-ride="carousel">
                    <ol class="carousel-indicators">
                        <li v-for="(item,i) in sideSlider" data-target="#carousel-home2" :data-slide-to="i" :class="{ active: i == 0 }" :key="item.SliderId"></li>
                    </ol>
                    <!--255*400 px-->
                    <div class="carousel-inner" role="listbox">
                        <div class="carousel-item" v-for="(item,i) in sideSlider" :class="{ active: i == 0 }" :key="item.SliderId">
                            <img :src="baseUrl+'/'+item.ImageFileName" alt="" class="img-fluid">
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
        </div>
    </div>
</template>

<script>
    export default {
        data() {
            return {
                baseUrl: "",
                catalog: [],
                mainSlider: [],
                sideSlider: []
            }
        },
        beforeMount() {
            //base url
            axios.get('/home/GetBaseUrl').then(response => {
                this.baseUrl = response.data;
            });


            axios.get('/home/GetCategory', { params: { place: "HomePageMain", numberOfData: 11 } }).then(response => {
                const { IsSuccess, Data } = response.data;
                if (!IsSuccess) return;

                this.catalog = Data;
            }).then(() => {
                axios.get('/home/GetSliderData', { params: { place: "Main" } }).then(response => {
                    const { IsSuccess, Data } = response.data;
                    if (!IsSuccess) return;

                    this.mainSlider = Data;
                }).then(() => {
                    axios.get('/home/GetSliderData', { params: { place: "Side" } }).then(response => {
                        const { IsSuccess, Data } = response.data;
                        if (!IsSuccess) return;

                        this.sideSlider = Data;
                    })
                })
            })
        }
    };
</script>