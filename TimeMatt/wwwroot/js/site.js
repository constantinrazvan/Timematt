(function () {
  var root = document.documentElement;
  var stored = localStorage.getItem("fh-theme");
  var systemDark = window.matchMedia("(prefers-color-scheme: dark)").matches;
  var theme = stored || (systemDark ? "dark" : "light");
  root.setAttribute("data-bs-theme", theme);

  document.addEventListener("DOMContentLoaded", function () {
    var toggle = document.getElementById("themeToggle");
    var icon = document.getElementById("themeToggleIcon");

    function updateIcon() {
      if (!icon) return;
      var current = root.getAttribute("data-bs-theme");
      icon.className = current === "dark" ? "bi bi-sun" : "bi bi-moon-stars";
    }
    updateIcon();

    if (toggle) {
      toggle.addEventListener("click", function () {
        var current = root.getAttribute("data-bs-theme");
        var next = current === "dark" ? "light" : "dark";
        root.setAttribute("data-bs-theme", next);
        localStorage.setItem("fh-theme", next);
        updateIcon();
      });
    }

    var sidebarToggle = document.getElementById("sidebarToggle");
    var sidebar = document.getElementById("fhSidebar");
    var overlay = document.getElementById("fhSidebarOverlay");

    function closeSidebar() {
      sidebar.classList.remove("show");
      overlay.classList.add("d-none");
    }

    if (sidebarToggle && sidebar && overlay) {
      sidebarToggle.addEventListener("click", function () {
        sidebar.classList.add("show");
        overlay.classList.remove("d-none");
      });
      overlay.addEventListener("click", closeSidebar);
    }
  });
})();
