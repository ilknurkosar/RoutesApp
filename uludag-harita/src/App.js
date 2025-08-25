import React, { useState, useEffect } from 'react';
import {
  Search, Menu, X, MapPin, Clock, Flower2, Navigation, User,
  Mountain, Car, Bike, Footprints, Zap, AlertCircle, RefreshCw
} from 'lucide-react';
import { MapContainer, TileLayer, Polyline, Marker, Popup } from 'react-leaflet';
import L from 'leaflet';
import 'leaflet/dist/leaflet.css';
import ApiService from './services/api';
import wellknown from 'wellknown';

// Leaflet icon fix for React
delete L.Icon.Default.prototype._getIconUrl;
L.Icon.Default.mergeOptions({
  iconRetinaUrl: 'https://cdnjs.cloudflare.com/ajax/libs/leaflet/1.7.1/images/marker-icon-2x.png',
  iconUrl: 'https://cdnjs.cloudflare.com/ajax/libs/leaflet/1.7.1/images/marker-icon.png',
  shadowUrl: 'https://cdnjs.cloudflare.com/ajax/libs/leaflet/1.7.1/images/marker-shadow.png',
});

// Uludağ koordinatları (merkez)
const ULUDAG_CENTER = [40.0917, 29.0750];
const DEFAULT_ZOOM = 13;

// WKT → Leaflet [lat, lon] listesi
const parseWktToLatLngs = (wkt) => {
  if (!wkt) return null;
  try {
    const geom = wellknown(wkt);
    if (!geom) return null;

    if (geom.type === 'LineString') {
      return geom.coordinates.map(([lon, lat]) => [lat, lon]);
    }
    if (geom.type === 'MultiLineString') {
      return geom.coordinates.flat().map(([lon, lat]) => [lat, lon]);
    }
    return null;
  } catch (e) {
    console.error('WKT parse hatası:', e);
    return null;
  }
};

// Rota tip belirleme
const getRouteType = (adi) => {
  if (!adi) return 'walking';
  const name = adi.toLowerCase();
  if (name.includes('bisiklet') || name.includes('bike')) return 'cycling';
  if (name.includes('araç') || name.includes('araba') || name.includes('car')) return 'driving';
  if (name.includes('atv') || name.includes('motor')) return 'atv';
  return 'walking';
};

const getRouteIcon = (type) => {
  switch (type) {
    case 'cycling': return Bike;
    case 'driving': return Car;
    case 'atv': return Zap;
    default: return Footprints;
  }
};

const getRouteColor = (type) => {
  switch (type) {
    case 'cycling': return '#3b82f6';
    case 'driving': return '#ef4444';
    case 'atv': return '#f59e0b';
    default: return '#22c55e';
  }
};

const UludagParkMap = () => {
  const [sidebarOpen, setSidebarOpen] = useState(false);
  const [userMenuOpen, setUserMenuOpen] = useState(false);
  const [activeLayer, setActiveLayer] = useState('all');
  const [searchQuery, setSearchQuery] = useState('');

  // API state'leri
  const [rotalar, setRotalar] = useState([]);          // detay + koordinat dahil
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  // API'den rotaları çek (liste + detay)
  useEffect(() => {
    const fetchRotalar = async () => {
      try {
        setLoading(true);
        setError(null);

        // 1) Listeyi çek
        const list = await ApiService.getRotalar(); // [{id, adi, renk, mesafe, tahminiSureDakika}, ...]
        if (!Array.isArray(list)) {
          setRotalar([]);
          return;
        }

        // 2) Her rota için detay (geometryWkt) çek
        const withGeom = await Promise.all(
          list.map(async (r) => {
            try {
              const d = await ApiService.getRota(r.id); // { id, geometryWkt, ... }
              const coords = parseWktToLatLngs(d?.geometryWkt);
              return { ...r, geometryWkt: d?.geometryWkt ?? null, coordinates: coords };
            } catch {
              return { ...r, geometryWkt: null, coordinates: null };
            }
          })
        );

        setRotalar(withGeom);
      } catch (err) {
        console.error('Rotalar yüklenirken hata:', err);
        setError(`Rotalar yüklenirken hata oluştu: ${err.message ?? err}`);
      } finally {
        setLoading(false);
      }
    };

    fetchRotalar();
  }, []);

  // Rotaları tipine göre grupla (ve UI için alanları hazırla)
  const groupedRoutes = rotalar.reduce((groups, rota) => {
    const type = getRouteType(rota.adi);
    if (!groups[type]) {
      groups[type] = {
        name:
          type === 'walking'
            ? 'Yürüyüş Rotaları'
            : type === 'cycling'
            ? 'Bisiklet Rotaları'
            : type === 'driving'
            ? 'Araç Rotaları'
            : 'ATV Rotaları',
        color: getRouteColor(type),
        icon: getRouteIcon(type),
        paths: [],
      };
    }

    const mesafeKm =
      typeof rota.mesafe === 'number'
        ? `${(rota.mesafe / 1000).toFixed(2)} km`
        : 'Bilinmiyor';

    const sure =
      typeof rota.tahminiSureDakika === 'number'
        ? rota.tahminiSureDakika >= 60
          ? `${Math.floor(rota.tahminiSureDakika / 60)} saat ${rota.tahminiSureDakika % 60} dakika`
          : `${rota.tahminiSureDakika} dakika`
        : 'Bilinmiyor';

    groups[type].paths.push({
      id: rota.id,
      name: rota.adi || 'İsimsiz Rota',
      distance: mesafeKm,
      duration: sure,
      color: rota.renk || getRouteColor(type),
      coordinates: rota.coordinates,
    });

    return groups;
  }, {});

  // Özel etkinlikler
  const specialEvents = [
    { id: 1, type: 'eclipse', date: '2025-03-29', time: '10:30', location: 'Sarıalan Bölgesi', description: 'Kısmi Güneş Tutulması', coordinates: [40.1000, 29.0800] },
    { id: 2, type: 'bloom', date: '2025-04-15', location: 'Kazıklıbent Yaylası', description: 'Uludağ Lale Çiçekleri', coordinates: [40.0850, 29.0900] },
    { id: 3, type: 'bloom', date: '2025-05-20', location: 'Kirazlıyayla', description: 'Rododendron Çiçekleri', coordinates: [40.0980, 29.0600] },
    { id: 4, type: 'eclipse', date: '2025-09-17', time: '14:45', location: 'Zirve Bölgesi', description: 'Kısmi Güneş Tutulması', coordinates: [40.1100, 29.0750] },
  ];

  const handleRetry = async () => {
    setError(null);
    setLoading(true);
    try {
      const list = await ApiService.getRotalar();
      const withGeom = await Promise.all(
        (list || []).map(async (r) => {
          try {
            const d = await ApiService.getRota(r.id);
            const coords = parseWktToLatLngs(d?.geometryWkt);
            return { ...r, geometryWkt: d?.geometryWkt ?? null, coordinates: coords };
          } catch {
            return { ...r, geometryWkt: null, coordinates: null };
          }
        })
      );
      setRotalar(withGeom);
    } catch (err) {
      setError(`Rotalar yüklenirken hata oluştu: ${err.message ?? err}`);
    } finally {
      setLoading(false);
    }
  };

  // Loading
  if (loading) {
    return (
      <div className="h-screen w-full flex items-center justify-center bg-gradient-to-br from-emerald-100 via-blue-50 to-green-100">
        <div className="text-center">
          <div className="animate-spin rounded-full h-16 w-16 border-4 border-green-500 border-t-transparent mx-auto"></div>
          <p className="mt-4 text-gray-600 font-medium">Uludağ haritası yükleniyor...</p>
          <p className="text-sm text-gray-500 mt-2">Rotalar API'den çekiliyor</p>
        </div>
      </div>
    );
  }

  // Error
  if (error) {
    return (
      <div className="h-screen w-full flex items-center justify-center bg-gradient-to-br from-red-100 via-orange-50 to-yellow-100">
        <div className="text-center max-w-md mx-4">
          <AlertCircle className="w-16 h-16 text-red-500 mx-auto mb-4" />
          <h2 className="text-xl font-bold text-gray-800 mb-2">Bağlantı Hatası</h2>
          <p className="text-red-600 mb-4 text-sm">{error}</p>
          <div className="space-y-2">
            <button
              onClick={handleRetry}
              className="inline-flex items-center gap-2 px-6 py-3 bg-green-500 hover:bg-green-600 text-white rounded-lg font-medium transition-colors"
            >
              <RefreshCw className="w-4 h-4" />
              Yeniden Dene
            </button>
            <p className="text-xs text-gray-500">
              Backend çalışıyor mu? http://localhost:5217/api/RotaTanim
            </p>
          </div>
        </div>
      </div>
    );
  }

  return (
    <div className="h-screen w-full bg-gray-50 relative overflow-hidden">
      {/* Ana Harita Alanı */}
      <div className="absolute inset-0">
        <MapContainer
          center={ULUDAG_CENTER}
          zoom={DEFAULT_ZOOM}
          className="w-full h-full"
          zoomControl={false}
        >
          <TileLayer
            url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
            attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
          />

          {/* Rotalar */}
          {Object.entries(groupedRoutes).map(([type, routeGroup]) => {
            if (activeLayer !== 'all' && activeLayer !== type) return null;

            return routeGroup.paths.map((path) => {
              if (!path.coordinates || path.coordinates.length === 0) return null;

              return (
                <Polyline
                  key={`route-${path.id}`}
                  positions={path.coordinates}
                  color={path.color}
                  weight={4}
                  opacity={0.8}
                >
                  <Popup>
                    <div className="p-2">
                      <h3 className="font-bold text-gray-800">{path.name}</h3>
                      <p className="text-sm text-gray-600">Mesafe: {path.distance}</p>
                      <p className="text-sm text-gray-600">Süre: {path.duration}</p>
                      <p className="text-xs text-gray-500 mt-1">ID: {path.id}</p>
                    </div>
                  </Popup>
                </Polyline>
              );
            });
          })}

          {/* Özel Etkinlikler */}
          {specialEvents.map((event) => (
            <Marker key={event.id} position={event.coordinates}>
              <Popup>
                <div className="p-2">
                  <h3 className="font-bold text-gray-800">{event.description}</h3>
                  <p className="text-sm text-gray-600">{event.location}</p>
                  <p className="text-xs text-gray-500">
                    {event.date} {event.time}
                  </p>
                </div>
              </Popup>
            </Marker>
          ))}
        </MapContainer>

        {/* Harita Başlığı */}
        <div className="absolute top-4 left-4 z-[1000] bg-white/90 backdrop-blur-sm rounded-lg p-3 shadow-md">
          <div className="flex items-center gap-2">
            <Mountain className="w-5 h-5 text-green-600" />
            <h3 className="font-bold text-gray-800">Uludağ Milli Parkı</h3>
            <span className="text-xs bg-green-100 text-green-700 px-2 py-1 rounded-full">
              {rotalar.length} rota
            </span>
          </div>
        </div>
      </div>

      {/* Sol Sidebar */}
      <div
        className={`absolute left-0 top-0 h-full bg-white shadow-2xl z-[1000] transition-all duration-300 ease-in-out ${
          sidebarOpen ? 'w-96' : 'w-0'
        } overflow-hidden`}
      >
        <div className="p-6 h-full overflow-y-auto">
          <div className="flex items-center justify-between mb-6">
            <h2 className="text-2xl font-bold text-gray-800">Uludağ Keşfi</h2>
            <button
              onClick={() => setSidebarOpen(false)}
              className="p-2 hover:bg-gray-100 rounded-lg transition-colors"
            >
              <X className="w-5 h-5" />
            </button>
          </div>

          {/* API Durumu */}
          <div className="bg-green-50 border border-green-200 rounded-lg p-3 mb-4">
            <div className="flex items-center gap-2 text-green-700">
              <div className="w-2 h-2 bg-green-500 rounded-full animate-pulse"></div>
              <span className="text-sm font-medium">API Bağlantısı Aktif</span>
            </div>
            <p className="text-xs text-green-600 mt-1">{rotalar.length} rota yüklendi</p>
          </div>

          {/* Arama */}
          <div className="relative mb-6">
            <Search className="absolute left-3 top-1/2 -translate-y-1/2 w-4 h-4 text-gray-400" />
            <input
              type="text"
              placeholder="Konum ara..."
              value={searchQuery}
              onChange={(e) => setSearchQuery(e.target.value)}
              className="w-full pl-10 pr-4 py-3 border border-gray-200 rounded-xl focus:ring-2 focus:ring-green-500 focus:border-transparent outline-none"
            />
          </div>

          {/* Rota Listesi */}
          {Object.entries(groupedRoutes).map(([type, routeGroup]) => {
            if (activeLayer !== 'all' && activeLayer !== type) return null;

            return (
              <div key={type} className="mb-6">
                <h4 className="font-semibold text-gray-700 flex items-center gap-2 mb-3">
                  <MapPin className="w-4 h-4" />
                  {routeGroup.name}
                </h4>

                <div className="space-y-3">
                  {routeGroup.paths.map((path) => (
                    <div
                      key={path.id}
                      className="bg-white rounded-lg p-4 shadow-sm hover:shadow-md transition-all cursor-pointer border border-gray-100 hover:border-gray-200"
                    >
                      <div className="flex justify-between items-start mb-2">
                        <h5 className="font-medium text-gray-800">{path.name}</h5>
                        <span className="px-2 py-1 rounded-full text-xs font-medium bg-blue-100 text-blue-700">
                          ID: {path.id}
                        </span>
                      </div>
                      <div className="text-sm text-gray-600 space-y-1">
                        <div className="flex justify-between">
                          <span>Mesafe:</span>
                          <span className="font-medium">{path.distance}</span>
                        </div>
                        <div className="flex justify-between">
                          <span>Süre:</span>
                          <span className="font-medium">{path.duration}</span>
                        </div>
                        {path.coordinates ? (
                          <div className="text-xs text-green-600">
                            ✓ Haritada görünüyor ({path.coordinates.length} nokta)
                          </div>
                        ) : (
                          <div className="text-xs text-red-600">⚠ Geometrik veri eksik</div>
                        )}
                      </div>
                    </div>
                  ))}
                </div>
              </div>
            );
          })}

          {/* Özel Etkinlikler */}
          <div className="space-y-3 mb-8">
            <h3 className="font-semibold text-gray-700 text-sm uppercase tracking-wide">
              Özel Etkinlikler
            </h3>
            {specialEvents.map((event) => (
              <div
                key={event.id}
                className="bg-gradient-to-r from-purple-50 to-pink-50 rounded-lg p-4 border border-purple-100"
              >
                <div className="flex items-start gap-3">
                  {event.type === 'eclipse' ? (
                    <Clock className="w-5 h-5 text-yellow-600 mt-0.5" />
                  ) : (
                    <Flower2 className="w-5 h-5 text-pink-600 mt-0.5" />
                  )}
                  <div className="flex-1">
                    <h5 className="font-medium text-gray-800 mb-1">{event.description}</h5>
                    <p className="text-sm text-gray-600 mb-1">{event.location}</p>
                    <div className="text-xs text-gray-500">
                      {event.date} {event.time && `- ${event.time}`}
                    </div>
                  </div>
                </div>
              </div>
            ))}
          </div>
        </div>
      </div>

      {/* Menü Toggle */}
      <button
        onClick={() => setSidebarOpen(!sidebarOpen)}
        className="absolute top-6 left-6 z-[1001] bg-white shadow-lg rounded-xl p-3 hover:shadow-xl transition-all duration-200 hover:scale-105"
      >
        <Menu className="w-6 h-6 text-gray-700" />
      </button>

      {/* Rota Katman Kontrolleri */}
      <div className="absolute top-6 left-20 z-[1000] flex gap-2 flex-wrap">
        {Object.entries(groupedRoutes).map(([key, route]) => {
          const IconComponent = route.icon;
          return (
            <button
              key={key}
              onClick={() => setActiveLayer(activeLayer === key ? 'all' : key)}
              className={`flex items-center gap-2 px-4 py-2 rounded-xl transition-all duration-200 font-medium text-sm shadow-lg hover:shadow-xl hover:scale-105 ${
                activeLayer === key
                  ? 'bg-white text-gray-800 ring-2 ring-opacity-50'
                  : 'bg-white/90 backdrop-blur-sm text-gray-700 hover:bg-white'
              }`}
              style={activeLayer === key ? { ringColor: route.color } : {}}
            >
              <IconComponent className="w-4 h-4" style={{ color: route.color }} />
              <span className="hidden sm:block">{route.name.replace(' Rotaları', '')}</span>
              <span className="bg-gray-100 text-gray-600 text-xs px-1.5 py-0.5 rounded-full">
                {route.paths.length}
              </span>
            </button>
          );
        })}

        <button
          onClick={() => setActiveLayer('all')}
          className={`px-4 py-2 rounded-xl transition-all duration-200 font-medium text-sm shadow-lg hover:shadow-xl hover:scale-105 ${
            activeLayer === 'all'
              ? 'bg-gradient-to-r from-green-500 to-blue-600 text-white'
              : 'bg-white/90 backdrop-blur-sm text-gray-700 hover:bg-white'
          }`}
        >
          Tümü ({rotalar.length})
        </button>
      </div>

      {/* Kullanıcı Menüsü */}
      <div className="absolute top-6 right-6 z-[1001]">
        <button
          onClick={() => setUserMenuOpen(!userMenuOpen)}
          className="bg-white shadow-lg rounded-full p-3 hover:shadow-xl transition-all duration-200 hover:scale-105"
        >
          <User className="w-6 h-6 text-gray-700" />
        </button>

        {userMenuOpen && (
          <div className="absolute right-0 mt-2 w-64 bg-white rounded-2xl shadow-2xl border border-gray-100 overflow-hidden">
            {/* user menu içeriği buraya */}
          </div>
        )}
      </div>

      {/* Konum Butonu */}
      <button className="absolute bottom-20 right-6 z-[1000] bg-white shadow-lg rounded-full p-4 hover:shadow-xl transition-all duration-200 hover:scale-105">
        <Navigation className="w-6 h-6 text-gray-700" />
      </button>

      {/* Alt Bilgi Paneli */}
      <div className="absolute bottom-6 left-1/2 -translate-x-1/2 z-[1000] bg-white/90 backdrop-blur-sm rounded-2xl shadow-lg p-4 max-w-md">
        <div className="text-center">
          <h4 className="font-semibold text-gray-800 mb-1">Uludağ Milli Parkı</h4>
          <p className="text-sm text-gray-600">API'den {rotalar.length} rota yüklendi</p>
          <div className="flex justify-center gap-4 mt-3">
            {Object.entries(groupedRoutes).map(([type, route]) => (
              <div key={type} className="flex items-center gap-1 text-xs text-gray-500">
                <div className="w-3 h-0.5" style={{ backgroundColor: route.color }}></div>
                <span>
                  {route.paths.length} {type}
                </span>
              </div>
            ))}
          </div>
        </div>
      </div>
    </div>
  );
};

export default UludagParkMap;
