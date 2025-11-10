// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const search = document.getElementById("search");
console.log(search);
search.addEventListener("input", (e) => {
  const searchValue = e.target.value;
  console.log(searchValue);
});
