// Maison — site scripts
document.addEventListener("DOMContentLoaded", function () {
  document.querySelectorAll(".table-card, .res-card, .data-panel, .page-hero").forEach(function (el, i) {
    el.style.animationDelay = (i * 0.04) + "s";
  });
});
