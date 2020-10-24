
import Vue from 'vue';
import MainSlider from './mainSlider.vue';
import SideSlider from './sideSlider.vue';
import HomeCategory from './catalog.vue';
import QuickDelivery from './quickDelivery.vue';
import TopCatalog from './topCatalog.vue';
import FlashDeals from './flashDeals.vue';


const vue = new Vue({
    el: "#homeApp",
    components: {
        MainSlider,
        SideSlider,
        HomeCategory,
        QuickDelivery,
        TopCatalog,
        FlashDeals
    }
});