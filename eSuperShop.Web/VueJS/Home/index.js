
import Vue from 'vue';
import MainSlider from './mainSlider.vue';
import SideSlider from './sideSlider.vue';


const vue = new Vue({
    el: "#homeApp",
    components: {
        MainSlider,
        SideSlider
    }
});