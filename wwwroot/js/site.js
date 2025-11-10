// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const search = document.getElementById("search");
const form = document.getElementById("search-form");
let debounceTimer;

document.addEventListener("DOMContentLoaded", () => {
  search.focus();
  const length = search.value.length;
  search.setSelectionRange(length, length);
});

search.addEventListener("input", (e) => {
  clearTimeout(debounceTimer);
  debounceTimer = setTimeout(() => {
    console.log("form submited");
    form.submit();
  }, 500);
});
