function createEventCard(event) {
    const fecha = new Date(event.eventDate).toLocaleDateString('es-AR', {
        day: '2-digit',
        month: 'long',
        year: 'numeric'
    });

    const hora = new Date(event.eventDate).toLocaleTimeString('es-AR', {
        hour: '2-digit',
        minute: '2-digit',
        hour12: false
    });

    const imageSrc = event.imageUrl 
        ? event.imageUrl 
        : 'https://images.unsplash.com/photo-1540039155733-5bb30b53aa14?w=400&q=80';

    return `
        <article class="event-card">
            <div class="card-image">
                <img src="${imageSrc}" alt="${event.name}"/>
                <div class="card-image-overlay"></div>
            </div>
            <div class="card-body">
                <h3 class="card-title">${event.name}</h3>
                <div class="card-meta">
                    <div class="card-meta-row">
                        <svg class="card-meta-icon" viewBox="0 0 24 24"><path d="M12 2C8.13 2 5 5.13 5 9c0 5.25 7 13 7 13s7-7.75 7-13c0-3.87-3.13-7-7-7zm0 9.5c-1.38 0-2.5-1.12-2.5-2.5s1.12-2.5 2.5-2.5 2.5 1.12 2.5 2.5-1.12 2.5-2.5 2.5z"/></svg>
                        ${event.venue}
                    </div>
                    <div class="card-meta-row">
                        <svg class="card-meta-icon" viewBox="0 0 24 24"><path d="M19 3h-1V1h-2v2H8V1H6v2H5c-1.11 0-2 .9-2 2v14c0 1.1.89 2 2 2h14c1.1 0 2-.9 2-2V5c0-1.1-.9-2-2-2zm0 16H5V8h14v11zM7 10h5v5H7z"/></svg>
                        ${fecha} · ${hora} hs
                    </div>
                </div>
            </div>
            <div class="card-rip"></div>
            <div class="card-footer">
                <button class="btn-reserve" onclick="goToSeatMap('${event.id}')">
                    Ver asientos
                </button>
            </div>
        </article>
    `;
}