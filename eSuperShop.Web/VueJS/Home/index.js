
import Vue from 'vue';
import SliderCatalog from './sliderCategory';

import QuickDelivery from './quickDelivery.vue';
import TopCatalog from './topCatalog.vue';
import FlashDeals from './flashDeals.vue';
import TopRated from './topRated.vue';
import TopStore from './topStore.vue';
import MoreToLove from './moreToLove.vue';


const vue = new Vue({
    el: "#homeApp",
    components: {
        SliderCatalog,
        QuickDelivery,
        TopCatalog,
        FlashDeals,
        TopRated,
        TopStore,
        MoreToLove
    }
});