
let currentPage = 1;
const PAGE_SIZE = 6;
let spinner, errorMessage, eventsContainer, paginationContainer;

// Funciones utilitarias

function showElement(element) {
    element.classList.remove("hidden");
    if (element.id === "spinner") element.classList.add("flex");
}

function hideElement(element) {
    element.classList.add("hidden");
    element.classList.remove("flex");
}

// Funciones de renderizado

function renderEvents(events) {
    eventsContainer.innerHTML = events
        .map(event => createEventCard(event))
        .join("");
}

function renderPagination(totalPages, currentPage) {
    paginationContainer.innerHTML = "";

    for (let i = 1; i <= totalPages; i++) {
        const button = document.createElement("button");
        button.textContent = i;
        button.className = i === currentPage ? "btn-page active" : "btn-page";
        button.onclick = () => loadEvents(i);
        paginationContainer.appendChild(button);
    }
}

//Redireccion a mapa de asientos

function goToSeatMap(eventId){
     window.location.href = `seat-map.html?eventId=${eventId}`;
}

// Función principal
async function loadEvents(page) {
    showElement(spinner);
    hideElement(eventsContainer);
    hideElement(errorMessage);
    hideElement(paginationContainer);

    try {
        const data = await getEvents(page, PAGE_SIZE);
        renderEvents(data.events);
        renderPagination(data.totalPages, page);
        showElement(eventsContainer);
        showElement(paginationContainer);
    } catch (error) {
        showElement(errorMessage);
    } finally {
        hideElement(spinner);
    }
}

// Arranque
document.addEventListener("DOMContentLoaded", () => {
    spinner = document.getElementById("spinner");
    errorMessage = document.getElementById("error-message");
    eventsContainer = document.getElementById("events-container");
    paginationContainer = document.getElementById("pagination-container");
    
    loadEvents(currentPage);
});